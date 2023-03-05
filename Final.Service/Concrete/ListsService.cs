using AutoMapper;
using Final.Base.Response;
using Final.Data.Model.DatabaseMongo;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Mongo.Abstarct;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.UnitOfWork;
using Final.Dto.Dtos;
using Final.Dto.Dtos.Create;
using Final.Service.Abstract;

namespace Final.Service.Concrete
{
    public class ListsService : BaseService<CreateListDto, Lists>, IListsService
    {
        private readonly IListsRepository _genericRepository;
        private readonly IUserService<UserDto> _userService;
        private readonly IListItemService _listItemService; 
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAgreeListRepository _agreeRepository;

        public ListsService(IListsRepository genericRepository, IUserService<UserDto> userService, IMapper mapper, IUnitOfWork unitOfWork, IListItemService listItemService, IAgreeListRepository agreeRepository) : base(genericRepository, mapper, unitOfWork)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _listItemService = listItemService;
            _agreeRepository = agreeRepository;
        }

        public async Task<BaseResponse<List<ListsDto>>> Search(int userId, string name, string catName)
        {
            List<Lists> lists;
            if(name is null && catName is null)
                lists = _genericRepository.Where(x => x.UserId == userId).ToList();
            else if(name is null)
                lists = _genericRepository.Where(x => x.CategoryName.Contains(catName) && x.UserId == userId).ToList();
            else if(catName is null)
                lists = _genericRepository.Where(x => x.Name.Contains(name) && x.UserId == userId).ToList();
            else
                lists = _genericRepository.Where(x => x.Name.Contains(name) && x.CategoryName.Contains(catName) && x.UserId == userId).ToList();
            return BaseResponse<List<ListsDto>>.Success(_mapper.Map<List<Lists>, List<ListsDto>>(lists), 200);
        }

        public async Task<BaseResponse<List<ListsDto>>> GetByUserIdAsync(int Userid)
        {
            //ilk user id var mı
            //var user = await _userRepository.GetByIdAsync(Userid);
            var user = await _userService.GetByIdAsync(Userid);

            if(user is null)
            {
                return BaseResponse<List<ListsDto>>.Fail("User is not found", 500);
            }

            var lists = _genericRepository.Where(x => x.UserId == Userid).ToList();
            //listeyi aldı
            if(lists is null)
            {
                return BaseResponse<List<ListsDto>>.Success(null, 200);
            }

            List<ListsDto> response = new List<ListsDto>();
            foreach (var item in lists)
            {
                ListsDto listsDto = new ListsDto();
                listsDto = _mapper.Map<Lists, ListsDto>(item);
                //burada bu listeye kayıtlı ListItem getirmem lazım.
                //var listItems = _listItemRepository.Where(x => x.ListsId == item.Id).ToList();
                var listItems = await _listItemService.GetListItemByListId(item.Id);
                listsDto.Items = _mapper.Map<List<ListItem>, List<ListItemDto>>(listItems.Data);
                response.Add(listsDto);
            }

            return BaseResponse<List<ListsDto>>.Success(response, 200);
        }

        public async Task<BaseResponse<string>> CompleteList(int listId, int userId)
        {
            //first check if all list items are complete
            var listItem = await _listItemService.GetListItemByListId(listId);
            if(listItem.Data.Count == 0)
                return BaseResponse<string>.Fail("first you need to add item", 406);

            var checkListItem = listItem.Data.Where(x => x.Receipt == false).FirstOrDefault();
            if(checkListItem is not null)
                return BaseResponse<string>.Fail("Items in the list are not complete", 406);


            //Hepsi tamam
            //şimdi liste ilk başta mongoDb ye eklenicek sonra silinecek
            var agreeList = await GetByListIdTurnAgreeListAsync(listId, listItem.Data);

            //write mongoDb
            agreeList.Data.Id = null;
            agreeList.Data.CompletionDate = DateTime.Now;
            var response = await _agreeRepository.CreateAsync(agreeList.Data);

            //delete list and item
            return await RemoveAsync(listId, userId);
        }

        public async Task<BaseResponse<AgreeList>> GetByListIdTurnAgreeListAsync(int listId, List<ListItem> listItems)
        {
            //find list.
            Lists list = await _genericRepository.GetByIdAsync(listId);
            if (list is null)
                return BaseResponse<AgreeList>.Fail("list is not valid", 404);

            var response = _mapper.Map<Lists, ListsDto>(list);
            response.Items = _mapper.Map<List<ListItem>, List<ListItemDto>>(listItems).ToList();

            return BaseResponse<AgreeList>.Success(_mapper.Map<ListsDto, AgreeList>(response),200);
        }

        public virtual async Task<BaseResponse<UpdateListDto>> UpdateAsync(int id, UpdateListDto updateResource, int userId)
        {
            // Validate Id is existent
            var tempEntity = await _genericRepository.GetByIdAsync(id);
            if (tempEntity is null)
                return BaseResponse<UpdateListDto>.Fail("NoData", 404);


            if(tempEntity.UserId != userId)
            {
                return BaseResponse<UpdateListDto>.Fail("you do not have permission", 403);
            }

            // Update infomation
            var mapped = _mapper.Map(updateResource, tempEntity);

            _genericRepository.Update(mapped);
            await _unitOfWork.CompleteAsync();

            // Mapping
            var resource = _mapper.Map<Lists, UpdateListDto>(mapped);

            return BaseResponse<UpdateListDto>.Success(resource, 200);
        }

        public virtual async Task<BaseResponse<string>> RemoveAsync(int id, int userId)
        {
            // Validate Id is existent
            var tempEntity = await _genericRepository.GetByIdAsync(id);
            if (tempEntity is null)
                return BaseResponse<string>.Fail("Id_NoData", 404);
            if(tempEntity.UserId != userId)
                return BaseResponse<string>.Fail("you do not have permission", 403);

            _genericRepository.RemoveAsync(tempEntity);

            //All items will be deleted when the list is deleted
            var items = await _listItemService.GetListItemByListId(tempEntity.Id);
            foreach (var item in items.Data)
            {
                _listItemService.RemoveAsync(item.Id);
            }

            await _unitOfWork.CompleteAsync();

            return BaseResponse<string>.Success("Delete success", 204);
        }

        public async Task<BaseResponse<List<AgreeList>>> AdminGetList()
        {
            return await _agreeRepository.GetAllAsync();
        }
    }
}


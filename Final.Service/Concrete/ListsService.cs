using System;
using AutoMapper;
using Final.Base.Model;
using Final.Base.Response;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.Repository.Sql.Concrete;
using Final.Data.UnitOfWork;
using Final.Dto.Dtos;
using Final.Dto.Dtos.Create;
using Final.Service.Abstract;
using SharpCompress.Common;

namespace Final.Service.Concrete
{
    public class ListsService : BaseService<CreateListDto, Lists>, IListsService
    {
        private readonly IListsRepository _genericRepository;
        private readonly IUserRepository _userRepository;
        private readonly IListItemRepository _listItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ListsService(IListsRepository genericRepository,IUserRepository userRepository, IListItemRepository listItemRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(genericRepository, mapper, unitOfWork)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _listItemRepository = listItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<ListsDto>>> GetByUserIdAsync(int Userid)
        {
            //ilk user id var mı
            var user = await _userRepository.GetByIdAsync(Userid);

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
                var listItems = _listItemRepository.Where(x => x.ListsId == item.Id).ToList();

                listsDto.Items = _mapper.Map<List<ListItem>, List<ListItemDto>>(listItems);
                response.Add(listsDto);
            }

            return BaseResponse<List<ListsDto>>.Success(response, 200);
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
            var items = _listItemRepository.Where(x => x.ListsId == tempEntity.Id);
            foreach (var item in items)
            {
                _listItemRepository.RemoveAsync(item);
            }

            await _unitOfWork.CompleteAsync();

            return BaseResponse<string>.Success("Delete success", 204);
        }
    }
}


using System;
using System.Data;
using AutoMapper;
using Final.Base.Model;
using Final.Base.Response;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.UnitOfWork;
using Final.Dto.Dtos;
using Final.Dto.Dtos.Create;
using Final.Service.Abstract;
using SharpCompress.Common;

namespace Final.Service.Concrete
{
	public class ListItemService : BaseService<CreateListItemDto, ListItem>, IListItemService
    {
        private readonly IListItemRepository genericRepository;
        private readonly IListsRepository listRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ListItemService(IListItemRepository genericRepository, IListsRepository listRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(genericRepository, mapper, unitOfWork)
        {
            this.genericRepository = genericRepository;
            this.listRepository = listRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<UpdateListItemDto>> UpdateReceipt(int ItemId, int userId)
        {
            //first item
            var item = await genericRepository.GetByIdAsync(ItemId);
            if(item is null)
            {
                return BaseResponse<UpdateListItemDto>.Fail("Item not found", 404);
            }

            var list = await listRepository.GetByIdAsync(item.ListsId);
            if(userId != list.UserId)
            {
                return BaseResponse<UpdateListItemDto>.Fail("you do not have permission", 403);
            }


            item.Receipt = true;
            genericRepository.Update(item);
            await unitOfWork.CompleteAsync();

            // Mapping
            var resource = mapper.Map<ListItem, UpdateListItemDto>(item);

            return BaseResponse<UpdateListItemDto>.Success(resource, 200);
        }

        public async Task<BaseResponse<List<ListItem>>> GetListItemByListId(int listId)
        {
            var items = genericRepository.Where(x => x.ListsId == listId).ToList();
            return BaseResponse<List<ListItem>>.Success(items, 200); 
        }
    }
}


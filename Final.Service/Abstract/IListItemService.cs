using System;
using Final.Base.Model;
using Final.Base.Response;
using Final.Data.Model.DatabaseSql;
using Final.Dto.Dtos;
using Final.Dto.Dtos.Create;

namespace Final.Service.Abstract
{
	public interface IListItemService: IBaseService<CreateListItemDto, ListItem>
	{
        Task<BaseResponse<UpdateListItemDto>> UpdateReceipt(int ItemId, int userId);
        public Task<BaseResponse<List<ListItem>>> GetListItemByListId(int listId);
    }

}


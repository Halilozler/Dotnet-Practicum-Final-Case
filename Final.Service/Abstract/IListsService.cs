using System;
using Final.Base.Model;
using Final.Base.Response;
using Final.Data.Model.DatabaseMongo;
using Final.Data.Model.DatabaseSql;
using Final.Dto.Dtos;
using Final.Dto.Dtos.Create;

namespace Final.Service.Abstract
{
	public interface IListsService: IBaseService<CreateListDto, ListItem>
    {
        Task<BaseResponse<List<ListsDto>>> GetByUserIdAsync(int Userid);
        public Task<BaseResponse<UpdateListDto>> UpdateAsync(int id, UpdateListDto updateResource, int userId);
        public Task<BaseResponse<string>> RemoveAsync(int id, int userId);
        public Task<BaseResponse<string>> CompleteList(int listId, int userId);
        public Task<BaseResponse<AgreeList>> GetByListIdTurnAgreeListAsync(int listId, List<ListItem> listItems);
        public Task<BaseResponse<List<AgreeList>>> AdminGetList();
        public Task<BaseResponse<List<ListsDto>>> Search(int userId, string name, string catName);
    }
}


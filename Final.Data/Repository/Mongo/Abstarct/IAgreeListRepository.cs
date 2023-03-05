using System;
using Final.Base.Response;
using Final.Data.Model.DatabaseMongo;

namespace Final.Data.Repository.Mongo.Abstarct
{
	public interface IAgreeListRepository
	{
        public Task<BaseResponse<List<AgreeList>>> GetAllAsync();
        public Task<BaseResponse<string>> CreateAsync(AgreeList list);
        public Task<BaseResponse<List<AgreeList>>> GetByUserId(int userId);
    }
}


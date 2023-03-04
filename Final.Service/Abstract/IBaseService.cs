using System;
using Final.Base.Model;
using Final.Base.Response;

namespace Final.Service.Abstract
{
	public interface IBaseService<Dto, Entity> where Dto : IDto where Entity : IEntity
    {
        Task<BaseResponse<Dto>> GetByIdAsync(int id);
        Task<BaseResponse<IEnumerable<Dto>>> GetAllAsync();
        Task<BaseResponse<Dto>> InsertAsync(Dto insertResource);
        Task<BaseResponse<Dto>> UpdateAsync(int id, Dto updateResource);
        Task<BaseResponse<Dto>> RemoveAsync(int id);
    }
}


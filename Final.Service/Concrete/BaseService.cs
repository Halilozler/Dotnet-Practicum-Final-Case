using System;
using AutoMapper;
using Final.Base.Model;
using Final.Base.Response;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.UnitOfWork;
using Final.Service.Abstract;

namespace Final.Service.Concrete
{
    public class BaseService<Dto, Entity> : IBaseService<Dto, Entity> where Dto : IDto where Entity : IEntity
    {
        private readonly IGenericRepository<Entity> genericRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public BaseService(IGenericRepository<Entity> genericRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public virtual async Task<BaseResponse<IEnumerable<Dto>>> GetAllAsync()
        {
            // Get list record from DB
            var tempEntity = await genericRepository.GetAllAsync();
            // Mapping Entity to Resource
            var result = mapper.Map<IEnumerable<Entity>, IEnumerable<Dto>>(tempEntity);

            return BaseResponse<IEnumerable<Dto>>.Success(result, 200);
        }

        public virtual async Task<BaseResponse<Dto>> GetByIdAsync(int id)
        {
            var tempEntity = await genericRepository.GetByIdAsync(id);
            // Mapping Entity to Resource
            var result = mapper.Map<Entity, Dto>(tempEntity);

            return BaseResponse<Dto>.Success(result, 200);
        }

        public virtual async Task<BaseResponse<Dto>> InsertAsync(Dto insertResource)
        {
            try
            {
                // Mapping Resource to Entity
                var tempEntity = mapper.Map<Dto, Entity>(insertResource);

                await genericRepository.InsertAsync(tempEntity);
                await unitOfWork.CompleteAsync();

                var mapped = mapper.Map<Entity, Dto>(tempEntity);

                return BaseResponse<Dto>.Success(mapped, 201);
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Saving_Error");
                return BaseResponse<Dto>.Fail("Saving_Error", 500);
            }
        }

        public virtual async Task<BaseResponse<Dto>> RemoveAsync(int id)
        {
            try
            {
                // Validate Id is existent
                var tempEntity = await genericRepository.GetByIdAsync(id);
                if (tempEntity is null)
                    return BaseResponse<Dto>.Fail("Id_NoData", 404);

                genericRepository.RemoveAsync(tempEntity);
                await unitOfWork.CompleteAsync();

                //return BaseResponse<Dto>.Success(mapper.Map<Entity, Dto>(tempEntity));
                return null;
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Deleting_Error");
                return BaseResponse<Dto>.Fail("Deleting_Error", 500);
            }
        }

        public virtual async Task<BaseResponse<Dto>> UpdateAsync(int id, Dto updateResource)
        {
            try
            {
                // Validate Id is existent
                var tempEntity = await genericRepository.GetByIdAsync(id);
                if (tempEntity is null)
                    return BaseResponse<Dto>.Fail("NoData", 404);
                // Update infomation
                var mapped = mapper.Map(updateResource, tempEntity);

                genericRepository.Update(mapped);
                await unitOfWork.CompleteAsync();

                // Mapping
                var resource = mapper.Map<Entity, Dto>(mapped);

                return BaseResponse<Dto>.Success(resource, 200);
            }
            catch (Exception ex)
            {
                //Log.Error(ex, "Updating_Error");
                return BaseResponse<Dto>.Fail("Updating_Error", 500);
            }
        }

    }
}



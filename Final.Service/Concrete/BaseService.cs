using System;
using AutoMapper;
using Final.Base.Model;
using Final.Base.Response;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.UnitOfWork;
using Final.Service.Abstract;

namespace Final.Service.Concrete
{
    public class BaseService<Dto, Entity> : IBaseService<Dto, Entity> where Dto : IEntity where Entity : IEntity
    {
        private readonly IGenericRepository<Entity> genericRepository;
        //private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public BaseService(IGenericRepository<Entity> genericRepository, IUnitOfWork unitOfWork)
        {
            this.genericRepository = genericRepository;
            //this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<Entity>> InsertAsync(Entity insertResource)
        {
            try
            {
                await genericRepository.InsertAsync(insertResource);
                await unitOfWork.CompleteAsync();

                return BaseResponse<Entity>.Success(insertResource, 201);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BaseResponse<Entity>.Fail("Saving_Error", 500);
            }
            
        }
    }
}


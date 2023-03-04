using System;
using AutoMapper;
using Final.Base.Model;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.UnitOfWork;
using Final.Dto.Dtos;
using Final.Service.Abstract;

namespace Final.Service.Concrete
{
	public class CategoryService : BaseService<CategoryDto, Category>, ICategoryService
    {
        private readonly IGenericRepository<Category> genericRepository;

        public CategoryService(IGenericRepository<Category> genericRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(genericRepository, mapper, unitOfWork)
        {
        }
    }
}


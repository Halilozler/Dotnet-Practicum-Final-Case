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
	public class RoleService : BaseService<RoleDto, Role>, IRoleService
    {
        private readonly IGenericRepository<Role> genericRepository;

        public RoleService(IGenericRepository<Role> genericRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(genericRepository, mapper, unitOfWork)
        {
        }
    }
}


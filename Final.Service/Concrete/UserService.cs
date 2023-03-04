using System;
using AutoMapper;
using Final.Base.Model;
using Final.Base.Response;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.UnitOfWork;
using Final.Dto.Dtos.Create;
using Final.Service.Abstract;
using SharpCompress.Common;

namespace Final.Service.Concrete
{
	public class UserService<UserDto> : BaseService<UserDto, User>, IUserService<UserDto> where UserDto : IDto
    {
        private readonly IGenericRepository<User> genericRepository;
        private readonly IMapper mapper;

        public UserService(IGenericRepository<User> genericRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(genericRepository, mapper, unitOfWork)
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
        }
    }
}


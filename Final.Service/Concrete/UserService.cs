using System;
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
	public class UserService<TUserDto> : BaseService<TUserDto, User>, IUserService<TUserDto> where TUserDto : IDto
    {
        private readonly IUserRepository genericRepository;
        private readonly IMapper mapper;
        private readonly IRoleService _roleService;

        public UserService(IUserRepository genericRepository, IMapper mapper, IUnitOfWork unitOfWork, IRoleService roleService) : base(genericRepository, mapper, unitOfWork)
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
            _roleService = roleService;
        }
        /*
        public override async Task<BaseResponse<TUserDto>> InsertAsync(TUserDto insertResource)
        {
            var role = await _roleService.GetByIdAsync(insertResource.RoleId);
            if (role.IsSuccessful == false)
            {
                return role;
            }
            return;
        }
        */
        public async Task<BaseResponse<bool>> Login(UserDto dto)
        {
            var user = await genericRepository.Control(dto);

            if(user is not null)
            {
                //token oluşturup dönmesi lazım.
                return BaseResponse<bool>.Success(true, 200);
            }
            return BaseResponse<bool>.Fail("User not found", 404);
        }

        //name ve password alıcak yani userDto.
        //kontrol edip geriye token döndürecek.

    }
}


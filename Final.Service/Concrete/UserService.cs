using System;
using AutoMapper;
using Final.Base.Model;
using Final.Base.Response;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.Repository.Sql.Concrete;
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
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUserRepository genericRepository, IMapper mapper, IUnitOfWork unitOfWork, IRoleService roleService) : base(genericRepository, mapper, unitOfWork)
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
            _roleService = roleService;
            this.unitOfWork = unitOfWork;
        }

        public virtual async Task<BaseResponse<TUserDto>> InsertAsync(CreateUserDto insertResource)
        {
            //role controle
            var role = await _roleService.GetByIdAsync(insertResource.RoleId);
            if (role.IsSuccessful == false)
            {
                return BaseResponse<TUserDto>.Fail("Role is not found", 404);
            }

            // Mapping Resource to Entity
            var tempEntity = mapper.Map<CreateUserDto, User>(insertResource);

            var user = genericRepository.Where(x => x.Name == insertResource.Name).ToList();

            if(user.Count() > 0)
            {
                return BaseResponse<TUserDto>.Fail("Name is exists", 404);
                //throw new InvalidOperationException("Name is exists");
            }

            await genericRepository.InsertAsync(tempEntity);
            await unitOfWork.CompleteAsync();

            var mapped = mapper.Map<User, TUserDto>(tempEntity);

            return BaseResponse<TUserDto>.Success(mapped, 201);
        }

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


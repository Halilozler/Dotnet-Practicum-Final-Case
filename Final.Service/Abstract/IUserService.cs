using System;
using Final.Base.Model;
using Final.Base.Response;
using Final.Data.Model.DatabaseSql;
using Final.Dto.Dtos;
using Final.Dto.Dtos.Create;

namespace Final.Service.Abstract
{
	public interface IUserService<TUserDto> : IBaseService<TUserDto, ListItem> where TUserDto : IDto
    {
        public Task<BaseResponse<bool>> Login(UserDto dto);
        public Task<BaseResponse<TUserDto>> InsertAsync(CreateUserDto insertResource);
    }
}


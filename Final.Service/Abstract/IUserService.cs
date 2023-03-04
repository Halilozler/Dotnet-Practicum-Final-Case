using System;
using Final.Base.Model;
using Final.Base.Response;
using Final.Data.Model.DatabaseSql;

namespace Final.Service.Abstract
{
	public interface IUserService<UserDto> : IBaseService<UserDto, ListItem> where UserDto : IDto
    {
        //Task<Boolean> NameControl(string Name);
    }
}


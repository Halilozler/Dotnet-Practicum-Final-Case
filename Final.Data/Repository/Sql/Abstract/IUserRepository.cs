using System;
using Final.Data.Model.DatabaseSql;
using Final.Dto.Dtos;

namespace Final.Data.Repository.Sql.Abstract
{
	public interface IUserRepository : IGenericRepository<User>
    {
		public Task<User> Control(UserDto dto);
	}
}


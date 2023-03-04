using System;
using Final.Data.Context;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;
using Final.Dto.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Final.Data.Repository.Sql.Concrete
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> Control(UserDto dto)
        {
            return await _context.User.SingleOrDefaultAsync(x => x.Name == dto.Name && x.Password == dto.Password);
        }
    }
}


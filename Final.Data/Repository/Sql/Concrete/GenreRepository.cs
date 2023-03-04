using System;
using Final.Data.Context;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;

namespace Final.Data.Repository.Sql.Concrete
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext context) : base(context)
        {
        }
    }
}


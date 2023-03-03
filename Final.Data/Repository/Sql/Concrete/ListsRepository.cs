using System;
using Final.Data.Context;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;

namespace Final.Data.Repository.Sql.Concrete
{
    public class ListsRepository : GenericRepository<Lists>, IListsRepository
    {
        public ListsRepository(AppDbContext context) : base(context)
        {
        }
    }
}


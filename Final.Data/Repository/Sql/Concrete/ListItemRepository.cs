using System;
using Final.Data.Context;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;

namespace Final.Data.Repository.Sql.Concrete
{
    public class ListItemRepository : GenericRepository<ListItem>, IListItemRepository
    {
        public ListItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}


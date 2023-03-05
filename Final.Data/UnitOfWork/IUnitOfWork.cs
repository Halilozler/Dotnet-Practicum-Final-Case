using System;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;

namespace Final.Data.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
        IGenericRepository<Lists> ListsRepository { get; }
        IGenericRepository<ListItem> ListItemRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        IGenericRepository<Genre> GenreRepository { get; }

        Task CompleteAsync();
    }
}


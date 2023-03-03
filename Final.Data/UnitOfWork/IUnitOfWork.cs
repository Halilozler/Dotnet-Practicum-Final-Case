using System;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;

namespace Final.Data.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
        IGenericRepository<Lists> ListsRepository { get; }

        Task CompleteAsync();
    }
}


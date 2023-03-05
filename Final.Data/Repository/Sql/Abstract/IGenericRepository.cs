﻿using System;
using Final.Base.Model;

namespace Final.Data.Repository.Sql.Abstract
{
	public interface IGenericRepository<TEntity> where TEntity : IEntity 
	{
        Task<TEntity> GetByIdAsync(int entityId);
        Task InsertAsync(TEntity entity);
        void RemoveAsync(TEntity entity);
        void Update(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> where);
    }
}


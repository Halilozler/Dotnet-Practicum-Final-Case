using System;
using System.Linq.Expressions;
using Final.Base.Model;
using Final.Data.Context;
using Final.Data.Repository.Sql.Abstract;
using Microsoft.EntityFrameworkCore;
using SharpCompress.Common;

namespace Final.Data.Repository.Sql.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly AppDbContext _context;
        private DbSet<TEntity> _entities;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int entityId)
        {
            return await _context.FindAsync<TEntity>(entityId);
        }

        public async Task InsertAsync(TEntity entity)
        {
            //Insert
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void RemoveAsync(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            //Update
            _entities.Update(entity);
        }

        public IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> where)
        {
            return _entities.Where(where).AsQueryable();
        }
    }
}


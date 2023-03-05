using System;
using Final.Data.Context;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.Repository.Sql.Concrete;

namespace Final.Data.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
        private readonly AppDbContext dbContext;
        public bool disposed;

        public IGenericRepository<Lists> ListsRepository { get; private set; }

        public IGenericRepository<ListItem> ListItemRepository { get; private set; }

        public IGenericRepository<User> UserRepository { get; private set; }

        public IGenericRepository<Role> RoleRepository { get; private set; }

        public IGenericRepository<Genre> GenreRepository { get; private set; }

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;

            ListsRepository = new GenericRepository<Lists>(dbContext);
            ListItemRepository = new GenericRepository<ListItem>(dbContext);
            UserRepository = new GenericRepository<User>(dbContext);
            RoleRepository = new GenericRepository<Role>(dbContext);
            GenreRepository = new GenericRepository<Genre>(dbContext);
        }

        public async Task CompleteAsync()
        {
            using (var dbContextTransaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    //yani şöyle demek istiyoruz aslında mesela ard arda 3 işlem yapıldı hepsi için
                    //ayrı ayrı SaveChanges uygulanacağına 3 işlem sonunda saveChange uyguluyoruz ve memory i siliyoruz.
                    dbContext.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    // logging                    
                    dbContextTransaction.Rollback();
                }
            }
        }

        protected virtual void Clean(bool disposing)
        {
            //burada clean metodu biz üst üste isteklerde hep yeni bir obje tanımlanacağından memory şişmeye başlar buda bunu önler.
            //Dispose ederek.
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Clean(true);
            GC.SuppressFinalize(this);
        }
    }
}

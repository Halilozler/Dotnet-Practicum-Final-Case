using System;
using Final.Data.Model.DatabaseSql;
using Final.Data.Repository.Sql.Abstract;
using Final.Data.Repository.Sql.Concrete;
using Final.Data.UnitOfWork;
using Final.Service.Abstract;
using Final.Service.Concrete;

namespace Final_Case.Extension
{
	public static class StartUpDIExtension
	{
        public static void AddServiceDI(this IServiceCollection services)
        {
            //Dependency Injection
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Repository
            services.AddScoped<IGenericRepository<Lists>, GenericRepository<Lists>>();

            //Service
            services.AddScoped<IListsService, ListsService>();

            //Mapper
        }
	}
}


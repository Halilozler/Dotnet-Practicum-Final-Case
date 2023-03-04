using System;
using Final.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Final_Case.Extension
{
	public static class StartupDbContextExtension
	{
        public static void AddDbContextDI(this IServiceCollection services, IConfiguration configuration)
        {
            var dbtype = configuration.GetConnectionString("DbType");
            if (dbtype == "SQL-MONGO")
            {
                var dbConfig = configuration.GetConnectionString("DefaultConnection");
                
                services.AddDbContext<AppDbContext>(options => options
                   .UseSqlServer(dbConfig, cf =>
                   {
                       //Otomtik migration oluşturulması için.
                       cf.MigrationsAssembly("Final.Data");
                   })
                   );
                /*
                services.Configure<MongoDbSettings>(configuration.GetSection("MongoSettings"));
                services.AddSingleton<IMongoDbSettings>(sp =>
                {
                    return sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
                });
                */
            }
        }
    }
}


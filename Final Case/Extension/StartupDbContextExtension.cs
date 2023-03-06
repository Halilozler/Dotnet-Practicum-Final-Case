using System;
using Final.Data.Context;
using Final.Service;
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
                services.AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseSqlServer(dbConfig, configure =>
                    {
                        //migrationu nereden alıcak onu belirtik bizim Infrastructure yerinde.
                        configure.MigrationsAssembly("Final.Data");
                    });
                });
                /*connect sql
                services.AddDbContext<AppDbContext>(options => options
                   .UseSqlServer(dbConfig, cf =>
                   {
                       //Otomtik migration oluşturulması için.
                       //cf.MigrationsAssembly("Final.Data");
                   })
                   );
                */
                /*connect Redis
                services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
                services.AddSingleton<RedisService>(sp =>
                {
                    var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
                    var redis = new RedisService(redisSettings.Host, redisSettings.Port);

                    redis.Connect();
                    return redis;
                });
                */


            }
            else if(dbtype == "INMEMORY")
            {
                services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "ListDb"));
            }
        }

        public static void AddMongoContextDI(this IServiceCollection services, IConfiguration configuration)
        {
            //conect mongoDB
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoSettings"));
            services.AddSingleton<IMongoDbSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            });
        }
    }
}


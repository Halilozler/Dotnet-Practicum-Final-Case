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

                //connect sql
                services.AddDbContext<AppDbContext>(options => options
                   .UseSqlServer(dbConfig, cf =>
                   {
                       //Otomtik migration oluşturulması için.
                       //cf.MigrationsAssembly("Final.Data");
                   })
                   );

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

                /*
                //conect mongoDB
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


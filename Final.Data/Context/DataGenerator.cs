using System;
using Final.Data.Model.DatabaseSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Final.Data.Context
{
	public class DataGenerator
	{
        public static void Initialize(IServiceProvider serviceProvider)
        {
			using(var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
			{
				if (context.Role.Any())
				{
					return;
				}

				context.Role.AddRange(
					new Role { Name = "Normal" },
					new Role { Name = "Admin" }
					);

				context.Genre.AddRange(
					new Genre { Name = "Adet" },
					new Genre { Name = "Litre" },
					new Genre { Name = "Kilogram" },
					new Genre { Name = "Gram" }
					);

				context.Category.AddRange(
					new Category { Name = "Okul" },
                    new Category { Name = "Ev" },
                    new Category { Name = "Bahçe" },
                    new Category { Name = "Mutfak" }
					);

				context.SaveChanges();
			}
		}
	}
}


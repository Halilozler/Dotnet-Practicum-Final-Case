using System;
using Final.Data.Context;

namespace WebApi.UnitTests.TestSetup
{
	public static class Genre
	{
        public static void AddGenres(this AppDbContext context)
        {
           context.Genre.AddRange(
                    new Final.Data.Model.DatabaseSql.Genre { Name = "Adet" },
                    new Final.Data.Model.DatabaseSql.Genre { Name = "Litre" },
                    new Final.Data.Model.DatabaseSql.Genre { Name = "Kilogram" },
                    new Final.Data.Model.DatabaseSql.Genre { Name = "Gram" }
                    );
        }
    }
}


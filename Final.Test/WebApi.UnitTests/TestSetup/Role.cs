using System;
using Final.Data.Context;

namespace WebApi.UnitTests.TestSetup
{
    public static class Role
    {
        public static void AddRoles(this AppDbContext context)
        {
            context.Role.AddRange(
					new Final.Data.Model.DatabaseSql.Role { Name = "Normal" },
					new Final.Data.Model.DatabaseSql.Role { Name = "Admin" }
					);
        }
    }
}


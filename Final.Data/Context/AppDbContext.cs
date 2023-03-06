using System;
using System.Reflection;
using Final.Data.Model.DatabaseSql;
using Microsoft.EntityFrameworkCore;

namespace Final.Data.Context
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Lists> Lists { get; set; }
        public DbSet<ListItem> ListItem { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
    }
}


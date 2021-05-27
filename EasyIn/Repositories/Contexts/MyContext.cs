using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyIn.Repositories.Contexts
{
    public class MyContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Platform> Platform { get; set; }
        public DbSet<Credential> Credential { get; set; }
        public DbSet<QrCodeLogin> QrCodeLogin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder
                   .Model
                   .GetEntityTypes()
                   .SelectMany(
                    e => e.GetProperties()
                        .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }

            base.OnModelCreating(modelBuilder);
        }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
    }
}

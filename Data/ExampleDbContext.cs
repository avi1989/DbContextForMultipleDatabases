using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiDbContext.Data.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiDbContext.Data
{
    public class ExampleDbContext : DbContext
    {
        DatabaseType databaseType;
        private IConfiguration configuration;

        public ExampleDbContext(IConfiguration configuration, DatabaseType dbType)
        {
            this.configuration = configuration;
            this.databaseType = dbType;
        }

        public DbSet<Person> Person { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (this.databaseType == DatabaseType.PostgreSql)
            {
                var connectionString = configuration.GetConnectionString("PostgreSql");
                optionsBuilder.UseNpgsql(connectionString);
            }
            else
            {
                var connectionString = configuration.GetConnectionString("SqlServer");
                optionsBuilder.UseSqlServer(connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Write default stuff here. Any configuration here will be converted by the conventionConverter
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Person>().HasKey(x => x.Id);

            // Surname is actually called LastName in SQL server and last_name in Postgres. Since it is configured here
            // in PascalCase, it will automatically be converted for postgres
            modelBuilder.Entity<Person>().Property(x => x.SurName).HasColumnName("LastName");

            if (this.databaseType == DatabaseType.PostgreSql)
            {
                base.OnModelCreating(modelBuilder);
                var convention = new SnakeCaseConverter();
                convention.SetConvention(modelBuilder);
            }
            else
            {
                base.OnModelCreating(modelBuilder);
            }
        }
    }
}

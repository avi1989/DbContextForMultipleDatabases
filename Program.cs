using Microsoft.Extensions.Configuration;
using MultiDbContext.Data;
using System;

namespace MultiDbContext
{
    class Program
    {
        static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json");

            var configuration = configBuilder.Build();

            using var sqlServerDbContext = new ExampleDbContext(configuration, DatabaseType.SqlServer);
            using var postgresDbContext = new ExampleDbContext(configuration, DatabaseType.PostgreSql);
            var personToAdd = new Person("Tom", "Brady", new DateTime(1980, 10, 10));

            sqlServerDbContext.Person.Add(personToAdd);
            postgresDbContext.Person.Add(personToAdd);
            sqlServerDbContext.SaveChanges();
            postgresDbContext.SaveChanges();
        }
    }
}

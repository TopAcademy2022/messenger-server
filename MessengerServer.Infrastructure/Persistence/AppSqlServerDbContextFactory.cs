using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MessengerServer.Infrastructure.Persistence
{
    public class AppSqlServerDbContextFactory : IDesignTimeDbContextFactory<AppSqlServerDbContext>
    {
        public AppSqlServerDbContext CreateDbContext(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var builder = new DbContextOptionsBuilder<AppSqlServerDbContext>();

            var provider = configuration["DatabaseProvider"];
            var connectionString = configuration.GetConnectionString("Default");

            if (string.IsNullOrWhiteSpace(provider))
            {
                throw new Exception("Missing 'DatabaseProvider' config value.");
            }
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("Missing DB connection string");
            }

            switch (provider)
            {
                case "SqlServer":
                    builder.UseSqlServer(connectionString);
                    break;
                default:
                    throw new Exception($"Unsupported provider: {provider}");
            }

            return new AppSqlServerDbContext(builder.Options);
        }
    }
}

using messanger_server.Models;
using Microsoft.EntityFrameworkCore;

// Add documentation
namespace messanger_server.Services
{
    public class DatabaseConnection : DbContext
    {
        private const string _DATABASE_NAME = "product";

        private string? _databaseConnectionString;

        private string? GetConnectionString()
        {
            const string _DATABASE_SERVER_NAME_VARIABLE = "DATABASE_SERVER_NAME";

            const string _DATABASE_PORT_NAME_VARIABLE = "DATABASE_PORT";

            const string _DATABASE_USER_NAME_VARIABLE = "USER_NAME";

            const string _DATABASE_PASSWORD_NAME_VARIABLE = "USER_PASSWORD";

            string? databaseServerName = Environment.GetEnvironmentVariable($"{_DATABASE_SERVER_NAME_VARIABLE}");

            string? databasePort = Environment.GetEnvironmentVariable($"{_DATABASE_PORT_NAME_VARIABLE}");

            string? userName = Environment.GetEnvironmentVariable($"{_DATABASE_USER_NAME_VARIABLE}");

            string? userPassword = Environment.GetEnvironmentVariable($"{_DATABASE_PASSWORD_NAME_VARIABLE}");

            if (!String.IsNullOrEmpty(databaseServerName) && !String.IsNullOrEmpty(databasePort) &&
                !String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(userPassword))
            {
                return $"Server={databaseServerName},{databasePort};Database={_DATABASE_NAME};Integrated security=False;User Id={userName};Password={userPassword};Encrypt=False;TrustServerCertificate=True;";
            }

            return null;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(this._databaseConnectionString);
        }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<SecretKey> SecretKeys { get; set; } = null!;

        public DatabaseConnection()
        {
            this._databaseConnectionString = this.GetConnectionString();
			Console.WriteLine(this._databaseConnectionString);
            
            if (!String.IsNullOrEmpty(this._databaseConnectionString))
            {
                try
                {
                    this.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

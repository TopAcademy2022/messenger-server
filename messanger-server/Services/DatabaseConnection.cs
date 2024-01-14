using System.Text;
using Microsoft.EntityFrameworkCore;
using messanger_server.Models;

// Add documentation
namespace messanger_server.Services
{
    public class DatabaseConnection : DbContext
    {
        private const string _CONFIG_FILE_NAME = "docker-compose.yaml";

        private string _textConfigurationFile;

        private string _databaseConnectionString;

        private DatabaseTypes? _selectedDatabaseType;

        private enum DatabaseTypes
        {
            MSSQL = 0
        }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<SecretKey> SecretKeys { get; set; } = null!;

        private string GetTextFromConfigFile()
        {
            // Replace file path
            const string CONFIG_FILE_DIRECTORY = "./";
            string result = string.Empty;
            
            try
            {
                FileStream fileStream = File.OpenRead(CONFIG_FILE_DIRECTORY + _CONFIG_FILE_NAME);
                byte[] bufer = new byte[fileStream.Length];
                fileStream.Read(bufer);

                result = Encoding.Default.GetString(bufer);

                fileStream.Close();
            }
            catch(Exception ex)
            {
                // Log ex.Message
            }

            return result;
        }

        private DatabaseTypes? GetDatabaseType()
        {
            if (this._textConfigurationFile.Contains("sqldata"))
            {
                return DatabaseTypes.MSSQL;
            }

            return null;
        }

        private string GetConnectionString()
        {
            string result = string.Empty;

            if (this._selectedDatabaseType == DatabaseTypes.MSSQL)
            {
                // Add parse config file
                result = @"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;";
            }

            return result;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            switch (this._selectedDatabaseType)
            {
                case DatabaseTypes.MSSQL:
                    dbContextOptionsBuilder.UseSqlServer(this._databaseConnectionString);
                    break;
                default:
                    // Log error
                    break;
            }
        }

        public DatabaseConnection()
        {
            this._textConfigurationFile = this.GetTextFromConfigFile();
            this._selectedDatabaseType = this.GetDatabaseType();
            this._databaseConnectionString = this.GetConnectionString();
            // Add init database tables
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace MessengerServer.Infrastructure.Persistence
{
    public class AppSqliteDbContext : AppDbContextBase
    {
        public AppSqliteDbContext(DbContextOptions<AppSqliteDbContext> options) : base(options) { }
    }
}

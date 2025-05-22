using Microsoft.EntityFrameworkCore;

namespace MessengerServer.Infrastructure.Persistence
{
    public class AppSqlServerDbContext : AppDbContextBase
    {
        public AppSqlServerDbContext(DbContextOptions<AppSqlServerDbContext> options) : base(options) { }
    }
}

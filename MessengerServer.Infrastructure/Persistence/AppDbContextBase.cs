using Microsoft.EntityFrameworkCore;
using MessengerServer.Infrastructure.Models.Entities;

namespace MessengerServer.Infrastructure.Persistence
{
    /*!
     * @brief Connection to database
     */
    public class AppDbContextBase : DbContext
    {
        public DbSet<User> Users => Set<User>();

        public DbSet<Message> Messages => Set<Message>();

        public AppDbContextBase(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

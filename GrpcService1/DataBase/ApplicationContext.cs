using Microsoft.EntityFrameworkCore;
using Server.DataBase.Models;

namespace Server.DataBase
{
    public class ApplicationContext : DbContext 
    {
        public DbSet<Cache> Caches { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> dbContext) : base(dbContext)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cache>();
        }
    }
}

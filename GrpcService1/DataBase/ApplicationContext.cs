using Microsoft.EntityFrameworkCore;

namespace Server.DataBase
{
    public class ApplicationContext : DbContext 
    {
        public DbSet<Log> Logs { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>();
        }
    }
}

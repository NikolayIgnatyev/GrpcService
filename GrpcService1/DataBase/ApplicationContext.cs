using Microsoft.EntityFrameworkCore;

namespace Server.DataBase
{
    public class ApplicationContext : DbContext 
    {
        public DbSet<Log> Logs { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>();
        }
    }
}

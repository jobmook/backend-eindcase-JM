using CursusApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CursusApp.Backend.DataAccess
{
    public class CursusDbContext : DbContext
    {
        public DbSet<Cursus> Cursussen { get; set; }
        public DbSet<CursusInstantie> CursusInstanties { get; set; }

        public CursusDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer(@"Server=localhost;Database = CursusDb;Integrated Security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

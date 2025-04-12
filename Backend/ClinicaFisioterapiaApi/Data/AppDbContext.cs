using Microsoft.EntityFrameworkCore;
using ClinicaFisioterapiaApi.Models;

namespace ClinicaFisioterapiaApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
        }

        public DbSet<User> Users { get; set; }
    }
}

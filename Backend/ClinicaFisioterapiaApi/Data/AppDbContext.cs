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
            modelBuilder.Entity<Clinic>().ToTable("clinics");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Clinic> Clinics { get; set; } 

    }
}

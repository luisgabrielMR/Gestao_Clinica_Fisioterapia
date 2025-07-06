using ClinicaFisioterapiaApi.Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ClinicaFisioterapiaApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            // ✅ Mapeia explicitamente o campo de expiração do refresh token
            modelBuilder.Entity<User>().Property(u => u.RefreshTokenExpiration);
        }
    }
}

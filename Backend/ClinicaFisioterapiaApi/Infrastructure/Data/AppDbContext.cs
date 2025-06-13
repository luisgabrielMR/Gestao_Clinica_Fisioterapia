using ClinicaFisioterapiaApi.Domain.Entities;
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

            // ✅ Se quiser mapear explicitamente o RefreshTokenExpiration:
            modelBuilder.Entity<User>().Property(u => u.RefreshTokenExpiration);

            // 🚫 NÃO deve existir nenhuma linha com RefreshTokenExpiresAt:
            // modelBuilder.Entity<User>().Property(u => u.RefreshTokenExpiresAt);
        }
    }
}

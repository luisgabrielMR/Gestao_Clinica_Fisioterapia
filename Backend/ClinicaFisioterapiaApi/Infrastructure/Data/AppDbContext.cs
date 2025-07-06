using ClinicaFisioterapiaApi.Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ClinicaFisioterapiaApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Clinic> Clinics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Users
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .Property(u => u.RefreshTokenExpiration);

            // Clinics
            modelBuilder.Entity<Clinic>(entity =>
            {
                entity.ToTable("clinics");

                entity.Property(c => c.ClinicId).HasColumnName("ClinicId");
                entity.Property(c => c.Name).HasColumnName("Name");
                entity.Property(c => c.Address).HasColumnName("Address");
                entity.Property(c => c.Neighborhood).HasColumnName("Neighborhood");
                entity.Property(c => c.City).HasColumnName("City");
                entity.Property(c => c.State).HasColumnName("State");
                entity.Property(c => c.ZipCode).HasColumnName("ZipCode");
                entity.Property(c => c.CreatedAt).HasColumnName("Created_at");
                entity.Property(c => c.UpdatedAt).HasColumnName("updated_at");
                entity.Property(c => c.DeletedAt).HasColumnName("deleted_at");

                entity.HasQueryFilter(c => c.DeletedAt == null);
            });


        }
    }
}

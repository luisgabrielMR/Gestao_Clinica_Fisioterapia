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
        public DbSet<Person> People { get; set; }

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

                entity.Property(c => c.ClinicId).HasColumnName("clinic_id");
                entity.Property(c => c.Name).HasColumnName("name");
                entity.Property(c => c.Address).HasColumnName("address");
                entity.Property(c => c.Neighborhood).HasColumnName("neighborhood");
                entity.Property(c => c.City).HasColumnName("city");
                entity.Property(c => c.State).HasColumnName("state");
                entity.Property(c => c.ZipCode).HasColumnName("zip_code");
                entity.Property(c => c.CreatedAt).HasColumnName("created_at");
                entity.Property(c => c.UpdatedAt).HasColumnName("updated_at");
                entity.Property(c => c.DeletedAt).HasColumnName("deleted_at");

                entity.HasQueryFilter(c => c.DeletedAt == null);
            });

            // People
            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("people");

                entity.Property(p => p.PersonId).HasColumnName("person_id");
                entity.Property(p => p.FullName).HasColumnName("full_name");
                entity.Property(p => p.Cpf).HasColumnName("cpf");
                entity.Property(p => p.BirthDate).HasColumnName("birth_date");
                entity.Property(p => p.Phone).HasColumnName("phone");
                entity.Property(p => p.Email).HasColumnName("email");
                entity.Property(p => p.Address).HasColumnName("address");
                entity.Property(p => p.Neighborhood).HasColumnName("neighborhood");
                entity.Property(p => p.City).HasColumnName("city");
                entity.Property(p => p.State).HasColumnName("state");
                entity.Property(p => p.ZipCode).HasColumnName("zip_code");

                entity.Property(p => p.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp with time zone");

                entity.Property(p => p.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp with time zone");

                entity.Property(p => p.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp with time zone");

                entity.HasQueryFilter(p => p.DeletedAt == null);
            });
        }

    }
}

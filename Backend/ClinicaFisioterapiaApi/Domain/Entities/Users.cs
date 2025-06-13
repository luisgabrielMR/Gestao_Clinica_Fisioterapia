namespace ClinicaFisioterapiaApi.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiration { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public DateTime? LastLoginAt { get; set; }
    }
}

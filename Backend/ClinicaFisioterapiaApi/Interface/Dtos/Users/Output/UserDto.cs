using Domain.Enums;

namespace ClinicaFisioterapiaApi.Interface.Dtos.Users.Output
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public UserRole Role { get; set; }

    }
}

using Domain.Enums;

namespace ClinicaFisioterapiaApi.Interface.Dtos.Users.Input
{
    public class CreateUserDto
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public UserRole Role { get; set; }

    }
}

using Domain.Enums;

namespace ClinicaFisioterapiaApi.Interface.Dtos.Users.Input
{
    public class UpdateUserDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public UserRole? Role { get; set; } 
    }
}

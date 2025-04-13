using System.ComponentModel.DataAnnotations;

namespace ClinicaFisioterapiaApi.Dtos.Users
{
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}

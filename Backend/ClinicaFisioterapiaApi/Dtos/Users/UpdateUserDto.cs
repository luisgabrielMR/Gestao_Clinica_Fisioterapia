using System.ComponentModel.DataAnnotations;

namespace ClinicaFisioterapiaApi.Dtos.Users
{
    public class UpdateUserDto
    {
        [StringLength(100, ErrorMessage = "O nome do usuário deve ter no máximo {1} caracteres.")]
        public string? Name { get; set; }

        [StringLength(255, ErrorMessage = "A senha deve ter no máximo {1} caracteres.")]
        public string? Password { get; set; }
    }
}
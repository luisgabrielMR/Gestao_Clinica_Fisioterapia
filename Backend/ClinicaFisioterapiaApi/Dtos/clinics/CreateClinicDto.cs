using System.ComponentModel.DataAnnotations;

namespace ClinicaFisioterapiaApi.Dtos.clinics
{
    public class CreateClinicDto
    {
        [Required(ErrorMessage = "O nome da clínica é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome da clínica deve ter no máximo {1} caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O endereço da clínica é obrigatório.")]
        [StringLength(255, ErrorMessage = "O endereço da clínica deve ter no máximo {1} caracteres.")]
        public string Address { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "O bairro da clínica deve ter no máximo {1} caracteres.")]
        public string Neighborhood { get; set; } = string.Empty;

        [Required(ErrorMessage = "A cidade da clínica é obrigatória.")]
        [StringLength(100, ErrorMessage = "A cidade da clínica deve ter no máximo {1} caracteres.")]
        public string City { get; set; } = string.Empty;

        [StringLength(50, ErrorMessage = "O estado da clínica deve ter no máximo {1} caracteres.")]
        public string State { get; set; } = string.Empty;

        [StringLength(20, ErrorMessage = "O CEP da clínica deve ter no máximo {1} caracteres.")]
        public string Zipcode { get; set; } = string.Empty;
    }
}
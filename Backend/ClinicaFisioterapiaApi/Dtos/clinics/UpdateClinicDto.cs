using System.ComponentModel.DataAnnotations;

namespace ClinicaFisioterapiaApi.Dtos.clinics
{
    public class UpdateClinicDto
    {
        [StringLength(200, ErrorMessage = "O nome da clínica deve ter no máximo {1} caracteres.")]
        public string? Name { get; set; }

        [StringLength(255, ErrorMessage = "O endereço da clínica deve ter no máximo {1} caracteres.")]
        public string? Address { get; set; }

        [StringLength(100, ErrorMessage = "O bairro da clínica deve ter no máximo {1} caracteres.")]
        public string? Neighborhood { get; set; }

        [StringLength(100, ErrorMessage = "A cidade da clínica deve ter no máximo {1} caracteres.")]
        public string? City { get; set; }

        [StringLength(50, ErrorMessage = "O estado da clínica deve ter no máximo {1} caracteres.")]
        public string? State { get; set; }

        [StringLength(20, ErrorMessage = "O CEP da clínica deve ter no máximo {1} caracteres.")]
        public string? Zipcode { get; set; }
    }
}
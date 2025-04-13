using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaFisioterapiaApi.Models
{
    [Table("clinics")]
    public class Clinic
    {
        [Key]
        [Column("clinicid")]
        public int ClinicId { get; private set; } // Tornando o ID somente leitura após a criação

        private string _name = string.Empty;
        [Required(ErrorMessage = "O nome da clínica é obrigatório.")]
        [StringLength(200, ErrorMessage = "O nome da clínica deve ter no máximo {1} caracteres.")]
        [Column("name")]
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O nome da clínica não pode ser vazio.");
                }
                _name = value.Trim(); // Removendo espaços em branco extras
            }
        }

        private string _address = string.Empty;
        [Required(ErrorMessage = "O endereço da clínica é obrigatório.")]
        [StringLength(255, ErrorMessage = "O endereço da clínica deve ter no máximo {1} caracteres.")]
        [Column("address")]
        public string Address
        {
            get => _address;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O endereço da clínica não pode ser vazio.");
                }
                _address = value.Trim();
            }
        }

        private string _neighborhood = string.Empty;
        [StringLength(100, ErrorMessage = "O bairro da clínica deve ter no máximo {1} caracteres.")]
        [Column("neighborhood")]
        public string Neighborhood
        {
            get => _neighborhood;
            set => _neighborhood = value?.Trim() ?? string.Empty; // Permitindo nulo e trimando
        }

        private string _city = string.Empty;
        [Required(ErrorMessage = "A cidade da clínica é obrigatória.")]
        [StringLength(100, ErrorMessage = "A cidade da clínica deve ter no máximo {1} caracteres.")]
        [Column("city")]
        public string City
        {
            get => _city;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A cidade da clínica não pode ser vazia.");
                }
                _city = value.Trim();
            }
        }

        private string _state = string.Empty;
        [StringLength(50, ErrorMessage = "O estado da clínica deve ter no máximo {1} caracteres.")]
        [Column("state")]
        public string State
        {
            get => _state;
            set => _state = value?.Trim() ?? string.Empty; // Permitindo nulo e trimando
        }

        private string _zipcode = string.Empty;
        [StringLength(20, ErrorMessage = "O CEP da clínica deve ter no máximo {1} caracteres.")]
        [Column("zipcode")]
        public string Zipcode
        {
            get => _zipcode;
            set => _zipcode = value?.Trim() ?? string.Empty; // Permitindo nulo e trimando
        }

        // Construtor sem parâmetros (necessário para o Entity Framework)
        public Clinic() { }

        // Construtor para facilitar a criação de instâncias válidas
        public Clinic(string name, string address, string city)
        {
            Name = name;
            Address = address;
            City = city;
        }

        // Método para atualizar propriedades, permitindo uma lógica de atualização mais controlada
        public void Update(string? name = null, string? address = null, string? neighborhood = null, string? city = null, string? state = null, string? zipcode = null)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }
            if (!string.IsNullOrWhiteSpace(address))
            {
                Address = address;
            }
            if (neighborhood != null) // Permitindo limpar o valor
            {
                Neighborhood = neighborhood;
            }
            if (!string.IsNullOrWhiteSpace(city))
            {
                City = city;
            }
            if (state != null) // Permitindo limpar o valor
            {
                State = state;
            }
            if (zipcode != null) // Permitindo limpar o valor
            {
                Zipcode = zipcode;
            }
        }
    }
}
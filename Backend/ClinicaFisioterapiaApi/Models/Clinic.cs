using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaFisioterapiaApi.Models
{
    [Table("clinics")]
    public class Clinic
    {
        [Key]
        [Column("clinic_id")]
        public int ClinicId { get; private set; }

        [Required]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column("address")]
        public string Address { get; set; } = string.Empty;

        [Column("neighborhood")]
        public string Neighborhood { get; set; } = string.Empty;

        [Required]
        [Column("city")]
        public string City { get; set; } = string.Empty;

        [Column("state")]
        public string State { get; set; } = string.Empty;

        [Column("zip_code")]
        public string Zipcode { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Método de atualização
        public void Update(
            string? name = null,
            string? address = null,
            string? neighborhood = null,
            string? city = null,
            string? state = null,
            string? zipcode = null
        )
        {
            if (!string.IsNullOrWhiteSpace(name))
                Name = name;
            if (!string.IsNullOrWhiteSpace(address))
                Address = address;
            if (neighborhood != null)
                Neighborhood = neighborhood;
            if (!string.IsNullOrWhiteSpace(city))
                City = city;
            if (state != null)
                State = state;
            if (zipcode != null)
                Zipcode = zipcode;
        }
    }
}

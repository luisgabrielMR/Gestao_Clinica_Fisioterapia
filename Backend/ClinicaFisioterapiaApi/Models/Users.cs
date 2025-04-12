// Users.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaFisioterapiaApi.Models
{
    [Table("users")] 
    public class User
    {
        [Key]
        [Column("userid")]
        public int UserId { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column("password")]
        public string Password { get; set; } = string.Empty;
    }
}
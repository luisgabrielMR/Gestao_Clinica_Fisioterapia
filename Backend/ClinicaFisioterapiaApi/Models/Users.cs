using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicaFisioterapiaApi.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("userid")]
        public int UserId { get; private set; }

        private string _name = string.Empty;
        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do usuário deve ter no máximo {1} caracteres.")]
        [Column("name")]
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("O nome do usuário não pode ser vazio.");
                }
                _name = value.Trim();
            }
        }

        private string _password = string.Empty;
        [Required(ErrorMessage = "A senha é obrigatória.")]
        [StringLength(255, ErrorMessage = "A senha deve ter no máximo {1} caracteres.")]
        [Column("password")]
        public string Password
        {
            get => _password;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A senha não pode ser vazia.");
                }
                _password = value; 
            }
        }

        // Construtor sem parâmetros (EF Core)
        public User() { }

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public void Update(string? name = null, string? password = null)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }
            if (!string.IsNullOrWhiteSpace(password))
            {
                Password = password;
            }
        }
    }
}
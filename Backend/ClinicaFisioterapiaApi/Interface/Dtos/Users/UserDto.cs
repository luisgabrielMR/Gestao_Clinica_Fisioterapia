namespace ClinicaFisioterapiaApi.Interface.Dtos.Users
{
    public class UserDto
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}

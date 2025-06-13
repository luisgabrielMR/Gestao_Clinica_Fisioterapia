namespace ClinicaFisioterapiaApi.Interface.Dtos.Auth
{
    public class RefreshTokenRequestDto
    {
        public string Username { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}

namespace ClinicaFisioterapiaApi.Interface.Dtos.Auth
{
    public class RefreshTokenResponseDto
    {
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}

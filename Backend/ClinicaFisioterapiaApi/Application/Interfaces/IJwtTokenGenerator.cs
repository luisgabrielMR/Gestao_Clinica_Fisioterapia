namespace ClinicaFisioterapiaApi.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(int userId, string role, string username);
    }
}

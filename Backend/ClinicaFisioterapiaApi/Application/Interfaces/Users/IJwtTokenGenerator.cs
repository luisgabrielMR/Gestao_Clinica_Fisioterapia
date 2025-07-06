using Domain.Enums;

namespace ClinicaFisioterapiaApi.Application.Interfaces
{
    public interface IJwtTokenGenerator
{
    string GenerateToken(int userId, UserRole role, string username);
}
}

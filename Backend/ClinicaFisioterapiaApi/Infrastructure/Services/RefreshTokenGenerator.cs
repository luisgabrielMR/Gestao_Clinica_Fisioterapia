using ClinicaFisioterapiaApi.Application.Interfaces;
using System.Security.Cryptography;

namespace ClinicaFisioterapiaApi.Infrastructure.Services
{
    public class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }
    }
}

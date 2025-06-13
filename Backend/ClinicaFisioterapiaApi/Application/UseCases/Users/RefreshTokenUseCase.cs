using ClinicaFisioterapiaApi.Application.Interfaces;
using DomainUser = ClinicaFisioterapiaApi.Domain.Entities.User;

namespace ClinicaFisioterapiaApi.Application.UseCases.Users
{
    public class RefreshTokenUseCase
    {
        private readonly IUserRepository _userRepository;

        public RefreshTokenUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<DomainUser?> ExecuteAsync(string username, string refreshToken)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null)
                return null;

            // Verifica se o refresh token e sua expiração são válidos
            if (user.RefreshToken != refreshToken || user.RefreshTokenExpiration == null || user.RefreshTokenExpiration < DateTime.UtcNow)
                return null;

            // (Opcional) Atualizar o refresh token aqui, se desejar renovar.

            return user;
        }
    }
}

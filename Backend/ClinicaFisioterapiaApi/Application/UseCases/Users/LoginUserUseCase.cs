using ClinicaFisioterapiaApi.Application.Interfaces;
using ClinicaFisioterapiaApi.Interface.Dtos.Auth;
using ClinicaFisioterapiaApi.Domain.Entities;

namespace ClinicaFisioterapiaApi.Application.UseCases.Users
{
    public class LoginUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;

        public LoginUserUseCase(
            IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
        }

        public async Task<AuthResponseDto> ExecuteAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null || user.DeletedAt != null)
            {
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");
            }

            // Verifica a senha com BCrypt
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");
            }

            // Gera Token e RefreshToken
            string token = _jwtTokenGenerator.GenerateToken(user.UserId, user.Role, user.Username);
            string refreshToken = _refreshTokenGenerator.GenerateRefreshToken();

            // Atualiza o RefreshToken no banco
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7);
            user.LastLoginAt = DateTime.UtcNow;

            await _userRepository.UpdateUserAsync(user);

            // Retorna a resposta
            return new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }
    }
}

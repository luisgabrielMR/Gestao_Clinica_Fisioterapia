namespace ClinicaFisioterapiaApi.Application.UseCases.Users
{
    using System.Threading.Tasks;
    using ClinicaFisioterapiaApi.Application.Interfaces;
    using ClinicaFisioterapiaApi.Domain.Entities;
    using Microsoft.Extensions.Logging;

    public class GetUserByIdUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetUserByIdUseCase> _logger;

        public GetUserByIdUseCase(IUserRepository userRepository, ILogger<GetUserByIdUseCase> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<User?> ExecuteAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null || user.DeletedAt != null)
            {
                _logger.LogWarning("User with ID {UserId} not found or deleted", userId);
                return null;
            }

            _logger.LogInformation("Retrieved User {UserId}", userId);

            return user;
        }
    }
}

using ClinicaFisioterapiaApi.Application.Interfaces;

namespace ClinicaFisioterapiaApi.Application.UseCases.Users
{
    public class DeleteUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ExecuteAsync(int userId)
        {
            await _userRepository.DeleteUserAsync(userId);
        }
    }
}

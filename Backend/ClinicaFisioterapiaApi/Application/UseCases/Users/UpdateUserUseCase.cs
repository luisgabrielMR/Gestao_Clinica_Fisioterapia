using AutoMapper;
using ClinicaFisioterapiaApi.Application.Interfaces;
using ClinicaFisioterapiaApi.Interface.Dtos.Users;
using ClinicaFisioterapiaApi.Domain.Entities;

namespace ClinicaFisioterapiaApi.Application.UseCases.Users
{
    public class UpdateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto?> ExecuteAsync(UpdateUserDto request, int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null || user.DeletedAt != null)
            {
                return null; // Usuário não encontrado ou já deletado
            }

            // Atualizar os campos (se informados no request)
            if (!string.IsNullOrWhiteSpace(request.Username))
            {
                user.Username = request.Username;
            }

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                // Aqui você deveria aplicar um Hash na senha (exemplo com BCrypt)
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }

            // Se você usar Role no UpdateUserDto, pode validar e atualizar aqui.
            if (!string.IsNullOrWhiteSpace(request.Role))
            {
                user.Role = request.Role;
            }

            // Atualiza a data de modificação
            user.UpdatedAt = DateTime.UtcNow;

            // Atualizar no banco
            await _userRepository.UpdateUserAsync(user);

            // Retornar o UserDto atualizado
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }

        // Método extra para permitir que o Controller atualize apenas os tokens
        public async Task UpdateTokensAsync(User user)
        {
            await _userRepository.UpdateUserAsync(user);
        }
    }
}

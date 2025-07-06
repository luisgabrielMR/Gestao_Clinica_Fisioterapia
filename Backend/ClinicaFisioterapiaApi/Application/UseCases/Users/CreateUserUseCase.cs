using AutoMapper;
using ClinicaFisioterapiaApi.Application.Interfaces;
using ClinicaFisioterapiaApi.Interface.Dtos.Users.Input;
using ClinicaFisioterapiaApi.Interface.Dtos.Users.Output;
using ClinicaFisioterapiaApi.Domain.Entities;
using BCrypt.Net;

namespace ClinicaFisioterapiaApi.Application.UseCases.Users
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IMapper _mapper;

        public CreateUserUseCase(
            IUserRepository userRepository,
            IRefreshTokenGenerator refreshTokenGenerator,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _refreshTokenGenerator = refreshTokenGenerator;
            _mapper = mapper;
        }

        public async Task<UserDto> ExecuteAsync(CreateUserDto request)
        {
            // Mapear o DTO para a entidade User
            var user = _mapper.Map<User>(request);

            // Gerar o Hash da senha usando BCrypt (padrão de mercado)
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            // Gerar RefreshToken
            user.RefreshToken = _refreshTokenGenerator.GenerateRefreshToken();
            user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7);

            // Preencher auditoria
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            // Salvar no banco
            await _userRepository.CreateUserAsync(user);

            // Retornar UserDto
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}

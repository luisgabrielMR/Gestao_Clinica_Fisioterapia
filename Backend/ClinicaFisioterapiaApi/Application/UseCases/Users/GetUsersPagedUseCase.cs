using AutoMapper;
using ClinicaFisioterapiaApi.Application.Interfaces;
using ClinicaFisioterapiaApi.Interface.Dtos.Users.Output;
using ClinicaFisioterapiaApi.Domain.Entities;

namespace ClinicaFisioterapiaApi.Application.UseCases.Users
{
    public class GetUsersPagedUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersPagedUseCase(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<UserDto> Users, int TotalCount)> ExecuteAsync(int page, int size, string? sort, string? filter)
        {
            var users = await _userRepository.GetUsersPagedAsync(page, size, sort, filter);
            var totalCount = await _userRepository.GetUsersTotalCountAsync(filter);

            // Mapear os Users para UserDto
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            return (userDtos, totalCount);
        }
    }
}

using AutoMapper;
using ClinicaFisioterapiaApi.Application.Interfaces;
using ClinicaFisioterapiaApi.Application.UseCases.Users;
using ClinicaFisioterapiaApi.Interface.Dtos.Auth;
using ClinicaFisioterapiaApi.Interface.Dtos.Users.Input;
using ClinicaFisioterapiaApi.Interface.Dtos.Users.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DomainUser = ClinicaFisioterapiaApi.Domain.Entities.User;

namespace ClinicaFisioterapiaApi.Interface.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly CreateUserUseCase _createUserUseCase;
        private readonly DeleteUserUseCase _deleteUserUseCase;
        private readonly GetUserByIdUseCase _getUserByIdUseCase;
        private readonly GetUsersPagedUseCase _getUsersPagedUseCase;
        private readonly LoginUserUseCase _loginUserUseCase;
        private readonly RefreshTokenUseCase _refreshTokenUseCase;
        private readonly UpdateUserUseCase _updateUserUseCase;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IMapper _mapper;

        public UserController(
            CreateUserUseCase createUserUseCase,
            DeleteUserUseCase deleteUserUseCase,
            GetUserByIdUseCase getUserByIdUseCase,
            GetUsersPagedUseCase getUsersPagedUseCase,
            LoginUserUseCase loginUserUseCase,
            RefreshTokenUseCase refreshTokenUseCase,
            UpdateUserUseCase updateUserUseCase,
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator,
            IMapper mapper)
        {
            _createUserUseCase = createUserUseCase;
            _deleteUserUseCase = deleteUserUseCase;
            _getUserByIdUseCase = getUserByIdUseCase;
            _getUsersPagedUseCase = getUsersPagedUseCase;
            _loginUserUseCase = loginUserUseCase;
            _refreshTokenUseCase = refreshTokenUseCase;
            _updateUserUseCase = updateUserUseCase;
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _mapper = mapper;
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var userDto = await _createUserUseCase.ExecuteAsync(createUserDto);
            return Ok(userDto);
        }

        [HttpGet]
        [AllowAnonymous] // Apenas para testes — depois voltar para [Authorize(Roles = "Admin")]
        public async Task<ActionResult<object>> GetUsersPaged(int page = 1, int size = 10, string? sort = null, string? filter = null)
        {
            var (users, totalCount) = await _getUsersPagedUseCase.ExecuteAsync(page, size, sort, filter);

            return Ok(new
            {
                TotalCount = totalCount,
                Page = page,
                Size = size,
                Users = users
            });
        }

        [HttpGet("{userId}")]
        [AllowAnonymous] // Apenas para testes — depois voltar para [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> GetUserById(int userId)
        {
            var userDto = await _getUserByIdUseCase.ExecuteAsync(userId);
            if (userDto == null)
                return NotFound();

            return Ok(userDto);
        }

        [HttpPut("{userId}")]
        [AllowAnonymous] // Apenas para testes — depois voltar para [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserDto>> UpdateUser(int userId, [FromBody] UpdateUserDto updateUserDto)
        {
            var userDto = await _updateUserUseCase.ExecuteAsync(updateUserDto, userId);
            if (userDto == null)
                return NotFound();

            return Ok(userDto);
        }

        [HttpDelete("{userId}")]
        [AllowAnonymous] // Apenas para testes — depois voltar para [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _deleteUserUseCase.ExecuteAsync(userId);
            return NoContent();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginUserDto loginUserDto)
        {
            var result = await _loginUserUseCase.ExecuteAsync(loginUserDto.Username, loginUserDto.Password);

            return Ok(new AuthResponseDto
            {
                Token = result.Token,
                RefreshToken = result.RefreshToken
            });
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult<RefreshTokenResponseDto>> RefreshToken([FromBody] RefreshTokenRequestDto refreshTokenRequestDto)
        {
            DomainUser? user = await _refreshTokenUseCase.ExecuteAsync(refreshTokenRequestDto.Username, refreshTokenRequestDto.RefreshToken);

            if (user == null)
                return Unauthorized("Token inválido ou expirado.");

            string token = _jwtTokenGenerator.GenerateToken(user.UserId, user.Role, user.Username);
            string refreshToken = _refreshTokenGenerator.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiration = DateTime.UtcNow.AddDays(7);

            await _updateUserUseCase.UpdateTokensAsync(user);

            var response = new RefreshTokenResponseDto
            {
                Token = token,
                RefreshToken = refreshToken
            };

            return Ok(response);
        }
    }
}

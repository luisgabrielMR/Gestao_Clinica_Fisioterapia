using ClinicaFisioterapiaApi.Dtos.Users;

public interface IUserService
{
    Task<UserDto?> CreateUserAsync(CreateUserDto dto);
    Task<UserDto?> GetUserByIdAsync(int userId);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<bool> UpdateUserAsync(int userId, UpdateUserDto dto);
    Task<bool> DeleteUserAsync(int userId);
    Task<UserDto?> GetByUsernameAsync(string username);
    Task<bool> VerifyPasswordAsync(string username, string password);

}

namespace ClinicaFisioterapiaApi.Application.Interfaces
{
    using ClinicaFisioterapiaApi.Domain.Entities;

    public interface IUserRepository
    {
        Task CreateUserAsync(User user);
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByUsernameAndPasswordAsync(string username, string passwordHash);
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
        Task<IEnumerable<User>> GetUsersPagedAsync(int page, int size, string? sort, string? filter);
        Task<int> GetUsersTotalCountAsync(string? filter);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);

    }
}

using ClinicaFisioterapiaApi.Models;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByIdAsync(int userId);
    Task<IEnumerable<User>> GetAllAsync();
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<User?> GetByPersonIdAsync(int personId);
    Task<User?> GetByUsernameAsync(string username);
}

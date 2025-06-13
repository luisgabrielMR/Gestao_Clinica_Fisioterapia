using ClinicaFisioterapiaApi.Application.Interfaces;
using ClinicaFisioterapiaApi.Domain.Entities;
using ClinicaFisioterapiaApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaFisioterapiaApi.Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.DeletedAt == null);
        }

        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId && u.DeletedAt == null);

            if (user != null)
            {
                user.DeletedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId && u.DeletedAt == null);
        }

        public async Task<User?> GetUserByUsernameAndPasswordAsync(string username, string passwordHash)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>
                u.Username == username &&
                u.PasswordHash == passwordHash &&
                u.DeletedAt == null);
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>
                u.RefreshToken == refreshToken &&
                u.DeletedAt == null);
        }

        public async Task<IEnumerable<User>> GetUsersPagedAsync(int page, int size, string? sort, string? filter)
        {
            // Implementação básica com filtro e paginação
            IQueryable<User> query = _context.Users.Where(u => u.DeletedAt == null);

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(u => u.Username.Contains(filter));
            }

            // Exemplo simples: sem ordenação avançada
            query = query.OrderBy(u => u.UserId);

            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<int> GetUsersTotalCountAsync(string? filter)
        {
            IQueryable<User> query = _context.Users.Where(u => u.DeletedAt == null);

            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(u => u.Username.Contains(filter));
            }

            return await query.CountAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

    }
}

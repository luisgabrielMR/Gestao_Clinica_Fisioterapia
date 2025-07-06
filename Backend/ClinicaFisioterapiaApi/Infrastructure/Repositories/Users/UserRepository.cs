using ClinicaFisioterapiaApi.Application.Interfaces;
using ClinicaFisioterapiaApi.Common.Helpers;
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

        // 🔁 Reutilização da regra de soft delete
        private IQueryable<User> NotDeletedUsers()
        {
            return _context.Users.Where(u => u.DeletedAt == null);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await NotDeletedUsers()
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await NotDeletedUsers()
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User?> GetUserByUsernameAndPasswordAsync(string username, string passwordHash)
        {
            return await NotDeletedUsers()
                .FirstOrDefaultAsync(u =>
                    u.Username == username &&
                    u.PasswordHash == passwordHash);
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await NotDeletedUsers()
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<IEnumerable<User>> GetUsersPagedAsync(int page, int size, string? sort, string? filter)
        {
            var query = NotDeletedUsers();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(u => u.Username.Contains(filter));
            }

            // ✅ Ordenação dinâmica com fallback seguro
            query = query.ApplySorting(sort, "UserId");

            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<int> GetUsersTotalCountAsync(string? filter)
        {
            var query = NotDeletedUsers();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(u => u.Username.Contains(filter));
            }

            return await query.CountAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await NotDeletedUsers()
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user is not null)
            {
                user.DeletedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}

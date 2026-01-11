using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using SmartWallet.Models;

namespace SmartWallet.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) => _context = context;

        public async Task<User?> GetByCredentialsAsync(string username, string passwordHash, string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u =>
                    u.Username == username && u.PasswordHash == passwordHash && u.Email == email);
        }

        public async Task<bool> ExistsAsync(string username, string email)
        {
            return await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.Username == username || u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
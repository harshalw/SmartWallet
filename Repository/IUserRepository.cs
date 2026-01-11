using SmartWallet.Entities;

namespace SmartWallet.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByCredentialsAsync(string username, string passwordHash);
        Task<bool> ExistsAsync(string username, string email);
        Task<User> CreateAsync(User user);
        Task<User?> GetByIdAsync(int id);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}

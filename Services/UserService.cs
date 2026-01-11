using SmartWallet.DTO;
using SmartWallet.Models;
using SmartWallet.Repositories;

namespace SmartWallet.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) => _repo = repo;

        private static UserDto MapToDto(User u) =>
            new UserDto
            {
                UserId = u.UserId,
                Username = u.Username,
                Email = u.Email,
                CreatedAt = u.CreatedAt,
                IsActive = u.IsActive
            };

        public async Task<UserDto?> AuthenticateAsync(LoginDto login)
        {
            var user = await _repo.GetByCredentialsAsync(login.Username, login.PasswordHash, login.Email);
            if (user == null) return null;

            return MapToDto(user);
        }

        public async Task<UserDto?> CreateAsync(CreateUserDto dto)
        {
            var exists = await _repo.ExistsAsync(dto.Username, dto.Email);
            if (exists) return null;

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var created = await _repo.CreateAsync(user);
            return MapToDto(created);
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return null;
            return MapToDto(user);
        }

        public async Task<UserDto?> UpdateAsync(int id, CreateUserDto dto)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return null;

            user.Username = dto.Username;
            user.Email = dto.Email;
            user.PasswordHash = dto.PasswordHash;

            await _repo.UpdateAsync(user);
            return MapToDto(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;
            await _repo.DeleteAsync(user);
            return true;
        }
    }
}
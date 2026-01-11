using SmartWallet.DTO;
namespace SmartWallet.Services
{
    public interface IUserService
    {
        Task<UserDto?> AuthenticateAsync(LoginDto login);
        Task<UserDto?> CreateAsync(CreateUserDto dto);
        Task<UserDto?> GetByIdAsync(int id);
        Task<UserDto?> UpdateAsync(int id, CreateUserDto dto);
        Task<bool> DeleteAsync(int id);
    }
}   
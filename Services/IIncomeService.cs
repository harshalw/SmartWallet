using SmartWallet.DTO;

namespace SmartWallet.Services
{
    public interface IIncomeService
    {
        Task<IEnumerable<IncomeDto>> GetByUserAsync(int userId);
        Task<IncomeDto?> GetByIdAsync(int id);
        Task<IncomeDto> CreateAsync(CreateIncomeDto dto);
        Task<IncomeDto?> UpdateAsync(int id, CreateIncomeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
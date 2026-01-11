using SmartWallet.DTO;

namespace SmartWallet.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseDto>> GetByUserAsync(int userId);
        Task<ExpenseDto?> GetByIdAsync(int id);
        Task<ExpenseDto> CreateAsync(CreateExpenseDto dto);
        Task<ExpenseDto?> UpdateAsync(int id, CreateExpenseDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
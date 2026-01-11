using SmartWallet.Entities;

namespace SmartWallet.Repositories
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetByUserAsync(int userId, int typeId);
        Task<Expense?> GetByIdAsync(int id);
        Task<Expense> CreateAsync(Expense expense);
        Task UpdateAsync(Expense expense);
        Task DeleteAsync(Expense expense);
    }
}
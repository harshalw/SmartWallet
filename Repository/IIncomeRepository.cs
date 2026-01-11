using SmartWallet.Entities;

namespace SmartWallet.Repositories
{
    public interface IIncomeRepository
    {
        Task<IEnumerable<Income>> GetByUserAsync(int userId, int typeId);
        Task<Income?> GetByIdAsync(int id);
        Task<Income> CreateAsync(Income income);
        Task UpdateAsync(Income income);
        Task DeleteAsync(Income income);
    }
}
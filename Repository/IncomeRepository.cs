using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using SmartWallet.Entities;

namespace SmartWallet.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly AppDbContext _context;
        public IncomeRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Income>> GetByUserAsync(int userId, int typeId)
        {
            return await _context.Income
                .AsNoTracking()
                .Where(x => x.UserId == userId && x.TypeId == typeId)
                .ToListAsync();
        }

        public async Task<Income?> GetByIdAsync(int id)
        {
            return await _context.Income
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.IncomeId == id);
        }

        public async Task<Income> CreateAsync(Income income)
        {
            _context.Income.Add(income);
            await _context.SaveChangesAsync();
            return income;
        }

        public async Task UpdateAsync(Income income)
        {
            _context.Income.Update(income);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Income income)
        {
            _context.Income.Remove(income);
            await _context.SaveChangesAsync();
        }
    }
}
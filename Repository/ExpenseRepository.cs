using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using SmartWallet.Entities;

namespace SmartWallet.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;
        public ExpenseRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Expense>> GetByUserAsync(int userId, int typeId)
        {
            return await _context.Expenses
                .AsNoTracking()
                .Where(x => x.UserId == userId && x.TypeId == typeId)
                .ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(int id)
        {
            return await _context.Expenses
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ExpenseId == id);
        }

        public async Task<Expense> CreateAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return expense;
        }

        public async Task UpdateAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Expense expense)
        {
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }
    }
}
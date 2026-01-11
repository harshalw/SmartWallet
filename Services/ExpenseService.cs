using SmartWallet.DTO;
using SmartWallet.Entities;
using SmartWallet.Repositories;

namespace SmartWallet.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _repo;
        public ExpenseService(IExpenseRepository repo) => _repo = repo;

        public async Task<IEnumerable<ExpenseDto>> GetByUserAsync(int userId)
        {
            // existing code used TypeId == 2 for expenses
            var items = await _repo.GetByUserAsync(userId, 2);
            return items.Select(MapToDto);
        }

        public async Task<ExpenseDto?> GetByIdAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            return item == null ? null : MapToDto(item);
        }

        public async Task<ExpenseDto> CreateAsync(CreateExpenseDto dto)
        {
            var entity = new Expense
            {
                UserId = dto.UserId,
                TypeId = dto.TypeId,
                Amount = dto.Amount,
                Description = dto.Description,
                ExpenseDate = dto.ExpenseDate,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _repo.CreateAsync(entity);
            return MapToDto(created);
        }

        public async Task<ExpenseDto?> UpdateAsync(int id, CreateExpenseDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.UserId = dto.UserId;
            existing.TypeId = dto.TypeId;
            existing.Amount = dto.Amount;
            existing.Description = dto.Description;
            existing.ExpenseDate = dto.ExpenseDate;

            await _repo.UpdateAsync(existing);
            return MapToDto(existing);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            await _repo.DeleteAsync(existing);
            return true;
        }

        private static ExpenseDto MapToDto(Expense e) =>
            new ExpenseDto
            {
                ExpenseId = e.ExpenseId,
                UserId = e.UserId,
                TypeId = e.TypeId,
                Amount = e.Amount,
                Description = e.Description,
                ExpenseDate = e.ExpenseDate,
                CreatedAt = e.CreatedAt
            };
    }
}
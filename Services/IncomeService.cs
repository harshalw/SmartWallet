using SmartWallet.DTO;
using SmartWallet.Entities;
using SmartWallet.Repositories;

namespace SmartWallet.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _repo;
        public IncomeService(IIncomeRepository repo) => _repo = repo;

        public async Task<IEnumerable<IncomeDto>> GetByUserAsync(int userId)
        {
            // existing code used TypeId == 1 for incomes
            var items = await _repo.GetByUserAsync(userId, 1);
            return items.Select(MapToDto);
        }

        public async Task<IncomeDto?> GetByIdAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            return item == null ? null : MapToDto(item);
        }

        public async Task<IncomeDto> CreateAsync(CreateIncomeDto dto)
        {
            var entity = new Income
            {
                UserId = dto.UserId,
                TypeId = dto.TypeId,
                Amount = dto.Amount,
                Description = dto.Description,
                IncomeDate = dto.IncomeDate,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _repo.CreateAsync(entity);
            return MapToDto(created);
        }

        public async Task<IncomeDto?> UpdateAsync(int id, CreateIncomeDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.UserId = dto.UserId;
            existing.TypeId = dto.TypeId;
            existing.Amount = dto.Amount;
            existing.Description = dto.Description;
            existing.IncomeDate = dto.IncomeDate;

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

        private static IncomeDto MapToDto(Income i) =>
            new IncomeDto
            {
                IncomeId = i.IncomeId,
                UserId = i.UserId,
                TypeId = i.TypeId,
                Amount = i.Amount,
                Description = i.Description,
                IncomeDate = i.IncomeDate,
                CreatedAt = i.CreatedAt
            };
    }
}
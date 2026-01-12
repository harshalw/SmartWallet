using SmartWallet.DTO;
using SmartWallet.Entities;
using SmartWallet.Repositories;

namespace SmartWallet.Services
{
    public class TypeMasterService : ITypeMasterService
    {
        private readonly ITypeMasterRepository _repo;
        public TypeMasterService(ITypeMasterRepository repo) => _repo = repo;

        public async Task<IEnumerable<TypeMasterDto>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.Select(t => new TypeMasterDto
            {
                TypeId = t.TypeId,
                TypeName = t.TypeName,
                Description = t.Description,
               // IsIncome = t.IsIncome,
                IsActive = t.IsActive
            }).Where(x=>x.IsIncome == true
            );
        }

        public async Task<IEnumerable<TypeMasterDto>> GetAllExpenseTypesAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.Select(t => new TypeMasterDto
            {
                TypeId = t.TypeId,
                TypeName = t.TypeName,
                Description = t.Description,
                //IsIncome = t.IsIncome,
                IsActive = t.IsActive
            }).Where(x => x.IsIncome == false);
        }

        public async Task<TypeMasterDto?> GetByIdAsync(int id)
        {
            var t = await _repo.GetByIdAsync(id);
            return t == null ? null : new TypeMasterDto
            {
                TypeId = t.TypeId,
                TypeName = t.TypeName,
                Description = t.Description,
                IsActive = t.IsActive
            };
        }

        public async Task<TypeMasterDto> CreateAsync(CreateTypeMasterDto dto)
        {
            var entity = new TypeMaster
            {
                TypeName = dto.TypeName,
                Description = dto.Description,
                IsActive = dto.IsActive
            };

            var created = await _repo.CreateAsync(entity);
            return new TypeMasterDto
            {
                TypeId = created.TypeId,
                TypeName = created.TypeName,
                Description = created.Description,
                IsActive = created.IsActive
            };
        }

        public async Task<TypeMasterDto?> UpdateAsync(int id, CreateTypeMasterDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.TypeName = dto.TypeName;
            existing.Description = dto.Description;
            existing.IsActive = dto.IsActive;

            await _repo.UpdateAsync(existing);

            return new TypeMasterDto
            {
                TypeId = existing.TypeId,
                TypeName = existing.TypeName,
                Description = existing.Description,
                IsActive = existing.IsActive
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            await _repo.DeleteAsync(existing);
            return true;
        }
    }
}
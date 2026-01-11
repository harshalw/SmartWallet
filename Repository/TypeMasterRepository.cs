using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using SmartWallet.Entities;

namespace SmartWallet.Repositories
{
    public class TypeMasterRepository : ITypeMasterRepository
    {
        private readonly AppDbContext _context;
        public TypeMasterRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<TypeMaster>> GetAllAsync()
        {
            return await _context.TypeMaster
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TypeMaster?> GetByIdAsync(int id)
        {
            return await _context.TypeMaster
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TypeId == id);
        }

        public async Task<TypeMaster> CreateAsync(TypeMaster type)
        {
            _context.TypeMaster.Add(type);
            await _context.SaveChangesAsync();
            return type;
        }

        public async Task UpdateAsync(TypeMaster type)
        {
            _context.TypeMaster.Update(type);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TypeMaster type)
        {
            _context.TypeMaster.Remove(type);
            await _context.SaveChangesAsync();
        }
    }
}
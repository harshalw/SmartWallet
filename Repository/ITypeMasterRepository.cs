using SmartWallet.Entities;

namespace SmartWallet.Repositories
{
    public interface ITypeMasterRepository
    {
        Task<IEnumerable<TypeMaster>> GetAllAsync();
        Task<TypeMaster?> GetByIdAsync(int id);
        Task<TypeMaster> CreateAsync(TypeMaster type);
        Task UpdateAsync(TypeMaster type);
        Task DeleteAsync(TypeMaster type);
    }
}
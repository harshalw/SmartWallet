using SmartWallet.DTO;

namespace SmartWallet.Services
{
    public interface ITypeMasterService
    {
        Task<IEnumerable<TypeMasterDto>> GetAllAsync(); 
        Task<IEnumerable<TypeMasterDto>> GetAllExpenseTypesAsync();
        Task<TypeMasterDto?> GetByIdAsync(int id);
        Task<TypeMasterDto> CreateAsync(CreateTypeMasterDto dto);
        Task<TypeMasterDto?> UpdateAsync(int id, CreateTypeMasterDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
using System.ComponentModel.DataAnnotations;

namespace SmartWallet.Entities
{
    public class TypeMaster
    {
        [Key]
        public int TypeId { get; set; }
        public required string  TypeName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

      
    }

}

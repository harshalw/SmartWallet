namespace SmartWallet.DTO
{
    public class TypeMasterDto
    {
        public int TypeId { get; set; }
        public required string TypeName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
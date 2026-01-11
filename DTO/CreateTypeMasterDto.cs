namespace SmartWallet.DTO
{
    public class CreateTypeMasterDto
    {
        public required string TypeName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
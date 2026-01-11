namespace SmartWallet.DTO
{
    public class UserDto
    {
        public int UserId { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
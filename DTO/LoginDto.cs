namespace SmartWallet.DTO
{
    public class LoginDto
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
    }
}
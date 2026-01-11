namespace SmartWallet.DTO;

public class CreateUserDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
}
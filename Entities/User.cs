using System.ComponentModel.DataAnnotations;

namespace SmartWallet.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        //public ICollection<Income> Incomes { get; set; }
        //public ICollection<Expense> Expenses { get; set; }
    }

}

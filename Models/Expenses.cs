using System.ComponentModel.DataAnnotations;

namespace SmartWallet.Models
{
    public class Expense
    {
        [Key]
        public int ExpenseId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int TypeId { get; set; }
        public TypeMaster Type { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }


}

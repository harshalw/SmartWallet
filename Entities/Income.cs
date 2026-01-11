using System.ComponentModel.DataAnnotations;

namespace SmartWallet.Entities
{
    public class Income
    {
        [Key]
        public int IncomeId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int TypeId { get; set; }
        public TypeMaster Type { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime IncomeDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}

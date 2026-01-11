namespace SmartWallet.DTO
{
    public class CreateExpenseDto
    {
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;
    }
}
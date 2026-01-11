namespace SmartWallet.DTO
{
    public class ExpenseDto
    {
        public int ExpenseId { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
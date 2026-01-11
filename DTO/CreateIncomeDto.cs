namespace SmartWallet.DTO
{
    public class CreateIncomeDto
    {
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime IncomeDate { get; set; } = DateTime.UtcNow;
    }
}
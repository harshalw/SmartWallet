namespace SmartWallet.DTO
{
    public class IncomeDto
    {
        public int IncomeId { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime IncomeDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
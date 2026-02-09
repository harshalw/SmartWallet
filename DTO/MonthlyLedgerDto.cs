public class MonthlyLedgerDto
{
    public int UserId { get; set; }
    public string Month { get; set; }   // yyyy-MM
    public string TranType { get; set; }
    public decimal Amount { get; set; }
    public DateTime TranDate { get; set; }
}
using SmartWallet.DTO;

namespace SmartWallet.Services
{
    public interface IReportsService
    {
        Task<List<ReportSummaryDto>> GetIncomeSummary(int userId);
        Task<List<ReportSummaryDto>> GetExpenseSummary(int userId);
    }
}
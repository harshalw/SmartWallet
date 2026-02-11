using Microsoft.EntityFrameworkCore;
using SmartWallet.DTO;
using MyApi.Data;

namespace SmartWallet.Services
{
    public class ReportsService : IReportsService
    {
        private readonly AppDbContext _context;

        public ReportsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReportSummaryDto>> GetIncomeSummary(int userId)
        {
            return await _context.Income
                .Where(x => x.UserId == userId)
                .GroupBy(x => x.Type.TypeName)
                .Select(g => new ReportSummaryDto
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(x => x.Amount)
                })
                .ToListAsync();
        }

        public async Task<List<ReportSummaryDto>> GetExpenseSummary(int userId)
        {
            return await _context.Expenses
                .Where(x => x.UserId == userId)
                .GroupBy(x => x.Type.TypeName)
                .Select(g => new ReportSummaryDto
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(x => x.Amount)
                })
                .ToListAsync();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;

namespace SmartWallet.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ReportsController(AppDbContext context) => _context = context;

        [HttpGet("monthly/{userId}")]
        public async Task<IActionResult> MonthlyReport(int userId)
        {
            var income = _context.Income.Where(x => x.UserId == userId);
            var expense = _context.Expenses.Where(x => x.UserId == userId);

            var result = await
                (from i in income
                 join e in expense
                 on new { i.IncomeDate.Year, i.IncomeDate.Month }
                 equals new { e.ExpenseDate.Year, e.ExpenseDate.Month }
                 into gj
                 from e in gj.DefaultIfEmpty()
                 group new { i, e } by new { i.IncomeDate.Year, i.IncomeDate.Month } into g
                 select new
                 {
                     g.Key.Year,
                     g.Key.Month,
                     TotalIncome = g.Sum(x => x.i.Amount),
                     TotalExpense = g.Sum(x => x.e != null ? x.e.Amount : 0),
                     Net = g.Sum(x => x.i.Amount) - g.Sum(x => x.e != null ? x.e.Amount : 0)
                 })
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Month)
                .ToListAsync();

            return Ok(result);
        }
    }

}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using SmartWallet.DTO;

namespace SmartWallet.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ReportsController(AppDbContext context) => _context = context;

        [HttpGet("monthly/{userId}")]
        public List<MonthlyIncomeExpenseDto> GetMonthlyIncomeExpense(int userId)
        {
            var incomeQuery =
                _context.Income
                    .Where(i => i.UserId == userId)
                    .Select(i => new
                    {
                        i.UserId,
                        Year = i.IncomeDate.Year,
                        Month = i.IncomeDate.Month,
                        Income = i.Amount,
                        Expense = 0m
                    });

            var expenseQuery =
                _context.Expenses
                    .Where(e => e.UserId == userId)
                    .Select(e => new
                    {
                        e.UserId,
                        Year = e.ExpenseDate.Year,
                        Month = e.ExpenseDate.Month,
                        Income = 0m,
                        Expense = e.Amount
                    });

            return incomeQuery
                .Union(expenseQuery)
                .GroupBy(x => new { x.UserId, x.Year, x.Month })
                .Select(g => new MonthlyIncomeExpenseDto
                {
                    UserId = g.Key.UserId,
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalIncome = g.Sum(x => x.Income),
                    TotalExpense = g.Sum(x => x.Expense)
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToList();
        }

        [HttpGet("detailsReport/{userId}")]
        public List<MonthlyLedgerDto> GetReportDistrubuted(int userId)
        {
            var incomeQuery =
                _context.Income
                    .Where(i => i.UserId == userId)
                    .Select(i => new
                    {
                        i.UserId,
                        Year = i.IncomeDate.Year,
                        Month = i.IncomeDate.Month,
                        TranDate = i.IncomeDate,
                        TranType = "Income",
                        Amount = i.Amount
                    });

            var expenseQuery =
                _context.Expenses
                    .Where(e => e.UserId == userId)
                    .Select(e => new
                    {
                        e.UserId,
                        Year = e.ExpenseDate.Year,
                        Month = e.ExpenseDate.Month,
                        TranDate = e.ExpenseDate,
                        TranType = "Expense",
                        Amount = e.Amount
                    });

            var result = incomeQuery
                .Union(expenseQuery)
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                 .ThenBy(x => x.TranType)
                .ThenBy(x => x.TranDate)
                .Select(x => new MonthlyLedgerDto
                {
                    UserId = x.UserId,
                    Month = x.Month.ToString(), // Fix: convert int to string
                    TranType = x.TranType,
                    Amount = x.Amount,
                    TranDate = x.TranDate
                })
                .ToList();

            return result;
        }


    }

}

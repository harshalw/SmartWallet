using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using SmartWallet.Models;

namespace SmartWallet.Controllers
{
    [ApiController]
    [Route("api/expenses")]
    public class ExpensesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ExpensesController(AppDbContext context) => _context = context;

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            return Ok(await _context.Expenses
                .Where(x => x.UserId == userId && x.TypeId == 2)
                //.Include(x => x.Type)
                .ToListAsync());
        }
         
        [HttpPost]
        public async Task<IActionResult> Create(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return Ok(expense);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Expense expense)
        {
            if (id != expense.ExpenseId) return BadRequest();
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
            return Ok(expense);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null) return NotFound();

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}

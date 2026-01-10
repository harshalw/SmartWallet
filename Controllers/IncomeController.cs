using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using SmartWallet.Models;

namespace SmartWallet.Controllers
{
    [ApiController]
    [Route("api/income")]
    public class IncomeController : ControllerBase
    {
        private readonly AppDbContext _context;
        public IncomeController(AppDbContext context) => _context = context;

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            return Ok(await _context.Income
                .Where(x => x.UserId == userId && x.TypeId == 1)
                //.Include(x => x.Type)
                .ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Income income)
        {
            _context.Income.Add(income);
            await _context.SaveChangesAsync();
            return Ok(income);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Income income)
        {
            if (id != income.IncomeId) return BadRequest();
            _context.Income.Update(income);
            await _context.SaveChangesAsync();
            return Ok(income);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var income = await _context.Income.FindAsync(id);
            if (income == null) return NotFound();

            _context.Income.Remove(income);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}

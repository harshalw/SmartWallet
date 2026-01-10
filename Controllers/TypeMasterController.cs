using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using SmartWallet.Models;

namespace SmartWallet.Controllers
{
    [ApiController]
    [Route("api/types")]
    public class TypeMasterController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TypeMasterController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _context.TypeMasters.ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Create(TypeMaster type)
        {
            _context.TypeMasters.Add(type);
            await _context.SaveChangesAsync();
            return Ok(type);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TypeMaster type)
        {
            if (id != type.TypeId) return BadRequest();
            _context.TypeMasters.Update(type);
            await _context.SaveChangesAsync();
            return Ok(type);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var type = await _context.TypeMasters.FindAsync(id);
            if (type == null) return NotFound();

            _context.TypeMasters.Remove(type);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }

}

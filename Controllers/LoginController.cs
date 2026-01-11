using DockerDeep.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using SmartWallet.Models;

namespace SmartWallet.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LoginController(AppDbContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> Exist(User user)
        {
            var login = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == user.Username && x.PasswordHash == user.PasswordHash
                && x.Email == user.Email);
            if (login == null) return NotFound();

            return Ok(login);
        }


        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (user == null) return BadRequest();

            var exists = await _context.Users
                .AnyAsync(u => u.Username == user.Username || u.Email == user.Email);

            if (exists)
                return Conflict(new { message = "Duplicate user" });

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (id != user.UserId) return BadRequest();
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? NotFound() : Ok(user);
        }
    }

}

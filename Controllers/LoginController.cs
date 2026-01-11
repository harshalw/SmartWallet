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

        // POST api/users/login
        // Authenticates a user. Returns limited user info on success.
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            if (user == null) return BadRequest();

            var login = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Username == user.Username && x.PasswordHash == user.PasswordHash
                    && x.Email == user.Email);

            if (login == null) return NotFound();

            return Ok();
        }

        // POST api/users/register
        // Creates a new user. Returns limited user info.
        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (user == null) return BadRequest();

            var exists = await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.Username == user.Username || u.Email == user.Email);

            if (exists)
                return Conflict(new { message = "Duplicate user" });

            // Ensure created timestamp and active flag are set if not provided
            user.CreatedAt = user.CreatedAt == default ? DateTime.UtcNow : user.CreatedAt;
            user.IsActive = user.IsActive;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var result = new
            {
                user.UserId,
                user.Username,
                user.Email,
                user.CreatedAt,
                user.IsActive
            };

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            if (user == null || id != user.UserId) return BadRequest();

            var exists = await _context.Users.AnyAsync(u => u.UserId == id);
            if (!exists) return NotFound();

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            var result = new
            {
                user.UserId,
                user.Username,
                user.Email,
                user.CreatedAt,
                user.IsActive
            };

            return Ok(result);
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
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null) return NotFound();

            var result = new
            {
                user.UserId,
                user.Username,
                user.Email,
                user.CreatedAt,
                user.IsActive
            };

            return Ok(result);
        }
    }

}

using Microsoft.AspNetCore.Mvc;
using SmartWallet.DTO;
using SmartWallet.Services;


namespace SmartWallet.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;
        public LoginController(IUserService userService) => _userService = userService;

        // POST api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto logindto)
        {
            if (logindto == null) return BadRequest();
            var user = await _userService.AuthenticateAsync(logindto);
            if (user == null) return NotFound();
            return Ok();
        }

        // POST api/users/register
        [HttpPost("register")]
            public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            if (dto == null) return BadRequest();
            var created = await _userService.CreateAsync(dto);
            if (created == null) return Conflict(new { message = "Duplicate user" });
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateUserDto dto)
        {
            if (dto == null) return BadRequest();
            var updated = await _userService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _userService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}

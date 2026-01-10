using Microsoft.AspNetCore.Mvc;

namespace DockerDeep.Controllers
{
    using DockerDeep.Model;
    using global::MyApi.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
   

    namespace MyApi.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class EmployeesController : ControllerBase
        {
            private readonly AppDbContext _context;

            public EmployeesController(AppDbContext context)
            {
                _context = context;
            }

            
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
            {
                var employees = await _context.employee.ToListAsync();
                return Ok(employees);
            }

            // GET: api/employees/5
            [HttpGet("{id:int}")]
            public async Task<ActionResult<Employee>> GetEmployee(int id)
            {
                var employee = await _context.employee.FindAsync(id);

                if (employee == null)
                    return NotFound();

                return Ok(employee);
            }

            [HttpPost]
            public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
            {
                await _context.employee.AddAsync(employee);
                await _context.SaveChangesAsync();

                return Ok(employee);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
            {
                if (id != employee.empId)
                    return BadRequest("Employee ID mismatch");

                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _ = GetEmployees();
                return Ok();
            }
        }
    }

}

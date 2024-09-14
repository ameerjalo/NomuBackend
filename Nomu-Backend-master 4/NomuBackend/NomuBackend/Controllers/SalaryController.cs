using Microsoft.AspNetCore.Mvc;
using NomuBackend.Services;
namespace NomuBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _salaryService;

        public SalaryController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Salary>>> Get()
        {
            var salaries = await _salaryService.GetSalariesAsync();
            return Ok(salaries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Salary>> Get(string id)
        {
            var salary = await _salaryService.GetSalaryByIdAsync(id);
            if (salary == null)
            {
                return NotFound();
            }
            return Ok(salary);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Salary salary)
        {
            await _salaryService.CreateSalaryAsync(salary);
            return CreatedAtAction(nameof(Get), new { id = salary.Id }, salary);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Salary salary)
        {
            var existingSalary = await _salaryService.GetSalaryByIdAsync(id);
            if (existingSalary == null)
            {
                return NotFound();
            }
            await _salaryService.UpdateSalaryAsync(id, salary);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingSalary = await _salaryService.GetSalaryByIdAsync(id);
            if (existingSalary == null)
            {
                return NotFound();
            }
            await _salaryService.RemoveSalaryAsync(id);
            return NoContent();
        }
    }
}

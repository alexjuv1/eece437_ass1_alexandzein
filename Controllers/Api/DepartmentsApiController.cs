using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Data;
using UniversityApp.Models;
using UniversityApp.DTOs;
using Microsoft.AspNetCore.Authorization;


namespace UniversityApp.Controllers.Api
{
    [Authorize(Policy = "AdminOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;
        public DepartmentsApiController(ApplicationDbContext ctx) => _ctx = ctx;

        // GET api/departments?page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var query = _ctx.Departments.AsNoTracking();
            var total = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return Ok(new { total, page, pageSize, items });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> Get(int id)
        {
            var d = await _ctx.Departments.FindAsync(id);
            if (d == null) return NotFound();
            return d;
        }

        [HttpPost]
        public async Task<ActionResult<Department>> Create([FromBody] DepartmentCreateDto dto)
        {
            var dept = new Department { Name = dto.Name };
            _ctx.Departments.Add(dept);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = dept.DepartmentId }, dept);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DepartmentUpdateDto dto)
        {
            var dept = await _ctx.Departments.FindAsync(id);
            if (dept == null) return NotFound();

            dept.Name = dto.Name;
            await _ctx.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var d = await _ctx.Departments.FindAsync(id);
            if (d == null) return NotFound();
            _ctx.Departments.Remove(d);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}

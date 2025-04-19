using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Data;
using UniversityApp.Models;
using UniversityApp.DTOs;
using Microsoft.AspNetCore.Authorization;


namespace UniversityApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;
        public StudentsApiController(ApplicationDbContext ctx) => _ctx = ctx;

        // GET api/students?page=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery]int page = 1,
            [FromQuery]int pageSize = 10)
        {
            var q = _ctx.Students
                .Include(s => s.Department)
                .Include(s => s.StudentCourses)
                    .ThenInclude(sc => sc.Course)
                .AsNoTracking();

            var total = await q.CountAsync();
            var items = await q
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new { total, page, pageSize, items });
        }

        // GET api/students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            var student = await _ctx.Students
                .Include(s => s.Department)
                .Include(s => s.StudentCourses)
                    .ThenInclude(sc => sc.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null) return NotFound();
            return student;
        }

        // POST api/students
        [HttpPost]
        [Authorize(Policy="AdminOnly")]
        public async Task<ActionResult<Student>> Create([FromBody] StudentCreateDto dto)
        {
            var student = new Student
            {
                Name           = dto.Name,
                Email          = dto.Email,
                EnrollmentYear = dto.EnrollmentYear,
                DepartmentId   = dto.DepartmentId
            };
            _ctx.Students.Add(student);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = student.StudentId }, student);
        }

        // DELETE api/students/5
        [HttpDelete("{id}")]
        [Authorize(Policy="AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _ctx.Students.FindAsync(id);
            if (student == null) return NotFound();
            _ctx.Students.Remove(student);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}

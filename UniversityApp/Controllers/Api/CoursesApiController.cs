using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApp.Data;
using UniversityApp.Models;
using UniversityApp.DTOs;
using Microsoft.AspNetCore.Authorization;


namespace UniversityApp.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesApiController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;
        public CoursesApiController(ApplicationDbContext ctx) => _ctx = ctx;

        // GET api/courses?page=1&pageSize=10&deptId=2
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery]int page = 1,
            [FromQuery]int pageSize = 10,
            [FromQuery]int? deptId = null)
        {
            var q = _ctx.Courses
                .Include(c => c.Department)
                .Include(c => c.Professor)
                .AsNoTracking();

            if (deptId.HasValue) q = q.Where(c => c.DepartmentId == deptId);

            var total = await q.CountAsync();
            var items = await q
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(new { total, page, pageSize, items });
        }

        // GET api/courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> Get(int id)
        {
            var course = await _ctx.Courses
                .Include(c => c.Department)
                .Include(c => c.Professor)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CourseId == id);

            if (course == null) return NotFound();
            return course;
        }

        [HttpPost]
        [Authorize(Policy="AdminOnly")]
        public async Task<ActionResult<Course>> Create([FromBody] CourseCreateDto dto)
        {
            var course = new Course
            {
                Name         = dto.Name,
                Credits      = dto.Credits,
                DepartmentId = dto.DepartmentId,
                ProfessorId  = dto.ProfessorId
            };
            _ctx.Courses.Add(course);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = course.CourseId }, course);
        }


        // PUT api/courses/5
        [HttpPut("{id}")]
        [Authorize(Policy="AdminOnly")]
        public async Task<IActionResult> Update(int id, Course course)
        {
            if (id != course.CourseId) return BadRequest();
            _ctx.Entry(course).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/courses/5
        [HttpDelete("{id}")]
        [Authorize(Policy="AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _ctx.Courses.FindAsync(id);
            if (course == null) return NotFound();
            _ctx.Courses.Remove(course);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}

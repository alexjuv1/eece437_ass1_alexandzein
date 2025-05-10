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
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] int? deptId = null)
        {
            var q = _ctx.Courses
                .Include(c => c.Department)
                .Include(c => c.Professor)
                .Include(c => c.StudentCourses)
                    .ThenInclude(sc => sc.Student)
                .AsNoTracking();

            if (deptId.HasValue) q = q.Where(c => c.DepartmentId == deptId);

            var total = await q.CountAsync();
            var items = await q
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CourseDto
                {
                    CourseId = c.CourseId,
                    Name = c.Name,
                    Credits = c.Credits,
                    DepartmentName = c.Department.Name,
                    ProfessorName = c.Professor.Name,
                    Students = c.StudentCourses.Select(sc => new StudentDto
                    {
                        StudentId = sc.Student.StudentId,
                        Name = sc.Student.Name,
                        Email = sc.Student.Email,
                        EnrollmentYear = sc.Student.EnrollmentYear,
                        DepartmentName = sc.Student.Department.Name
                    }).ToList()
                })
                .ToListAsync();

            return Ok(new { total, page, pageSize, items });
        }

        // GET api/courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> Get(int id)
        {
            var course = await _ctx.Courses
                .Include(c => c.Department)
                .Include(c => c.Professor)
                .Include(c => c.StudentCourses)
                    .ThenInclude(sc => sc.Student)
                .Where(c => c.CourseId == id)
                .Select(c => new CourseDto
                {
                    CourseId = c.CourseId,
                    Name = c.Name,
                    Credits = c.Credits,
                    DepartmentName = c.Department.Name,
                    ProfessorName = c.Professor.Name,
                    Students = c.StudentCourses.Select(sc => new StudentDto
                    {
                        StudentId = sc.Student.StudentId,
                        Name = sc.Student.Name,
                        Email = sc.Student.Email,
                        EnrollmentYear = sc.Student.EnrollmentYear,
                        DepartmentName = sc.Student.Department.Name
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (course == null) return NotFound();
            return course;
        }

        // POST api/courses
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Course>> Create([FromBody] CourseCreateDto dto)
        {
            var course = new Course
            {
                Name = dto.Name,
                Credits = dto.Credits,
                DepartmentId = dto.DepartmentId,
                ProfessorId = dto.ProfessorId
            };
            _ctx.Courses.Add(course);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = course.CourseId }, course);
        }

        // PUT api/courses/5
        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(int id, [FromBody] Course course)
        {
            if (id != course.CourseId) return BadRequest();
            _ctx.Entry(course).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/courses/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
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

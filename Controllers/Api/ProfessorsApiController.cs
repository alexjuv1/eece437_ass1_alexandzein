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
    public class ProfessorsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;
        public ProfessorsApiController(ApplicationDbContext ctx) => _ctx = ctx;

        // GET api/professors
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var professors = await _ctx.Professors
                .Include(p => p.Department)
                .Select(p => new ProfessorDto
                {
                    ProfessorId   = p.ProfessorId,
                    Name          = p.Name,
                    Email         = p.Email,
                    DepartmentName = p.Department.Name
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(professors);
        }

        // GET api/professors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _ctx.Professors
                .Include(p => p.Department)
                .Where(p => p.ProfessorId == id)
                .Select(p => new ProfessorDto
                {
                    ProfessorId   = p.ProfessorId,
                    Name          = p.Name,
                    Email         = p.Email,
                    DepartmentName = p.Department.Name
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (p == null) return NotFound();
            return Ok(p);
        }

        // POST api/professors
        [HttpPost]
        [Authorize(Policy="AdminOnly")]
        public async Task<ActionResult<Professor>> Create([FromBody] ProfessorCreateDto dto)
        {
            var prof = new Professor
            {
                Name         = dto.Name,
                Email        = dto.Email,
                DepartmentId = dto.DepartmentId
            };
            _ctx.Professors.Add(prof);
            await _ctx.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = prof.ProfessorId }, prof);
        }

        // PUT api/professors/5
        [HttpPut("{id}")]
        [Authorize(Policy="AdminOnly")]
        public async Task<IActionResult> Update(int id, Professor prof)
        {
            if (id != prof.ProfessorId) return BadRequest();
            _ctx.Entry(prof).State = EntityState.Modified;
            await _ctx.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/professors/5
        [HttpDelete("{id}")]
        [Authorize(Policy="AdminOnly")]
        public async Task<IActionResult> Delete(int id)
        {
            var prof = await _ctx.Professors.FindAsync(id);
            if (prof == null) return NotFound();
            _ctx.Professors.Remove(prof);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}

// File: UniversityApp.Web/Controllers/CoursesController.cs

using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityApp.Application.DTOs;
using UniversityApp.Application.Features.Courses.Commands;
using UniversityApp.Application.Features.Courses.Queries;
using System.Threading.Tasks;

namespace UniversityApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CoursesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCoursesQuery());
            if (!result.Success) return BadRequest(result.Error);
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetCourseByIdQuery { CourseId = id });
            if (!result.Success) return NotFound(result.Error);
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto dto)
        {
            var cmd = new CreateCourseCommand { Course = dto };
            var result = await _mediator.Send(cmd);
            if (!result.Success) return BadRequest(result.Error);
            return CreatedAtAction(nameof(Get), new { id = result.Data.CourseId }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateCourseDto dto)
        {
            var cmd = new UpdateCourseCommand { CourseId = id, Updated = dto };
            var result = await _mediator.Send(cmd);
            if (!result.Success) return NotFound(result.Error);
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCourseCommand { CourseId = id });
            if (!result.Success) return NotFound(result.Error);
            return NoContent();
        }
    }
}

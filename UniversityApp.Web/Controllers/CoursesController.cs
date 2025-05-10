using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using UniversityApp.Application.DTOs;
using UniversityApp.Application.Features.Courses.Commands;
using UniversityApp.Application.Features.Courses.Queries;

namespace UniversityApp.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ✅ Supports pagination with ?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCoursesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCourseByIdQuery(id));
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseDto dto)
        {
            var result = await _mediator.Send(new CreateCourseCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");

            var result = await _mediator.Send(new UpdateCourseCommand(dto));
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCourseCommand(id));
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}

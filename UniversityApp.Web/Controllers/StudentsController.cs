using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniversityApp.Application.DTOs;
using UniversityApp.Application.Features.Students.Commands;
using UniversityApp.Application.Features.Students.Queries;
using System.Threading.Tasks;

namespace UniversityApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllStudentsQuery());
            if (!result.Success) return BadRequest(result.Error);
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetStudentByIdQuery { StudentId = id });
            if (!result.Success) return NotFound(result.Error);
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentDto dto)
        {
            var cmd = new CreateStudentCommand { Student = dto };
            var result = await _mediator.Send(cmd);
            if (!result.Success) return BadRequest(result.Error);
            return CreatedAtAction(nameof(Get), new { id = result.Data.StudentId }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateStudentDto dto)
        {
            var cmd = new UpdateStudentCommand { StudentId = id, UpdatedData = dto };
            var result = await _mediator.Send(cmd);
            if (!result.Success) return NotFound(result.Error);
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteStudentCommand { StudentId = id });
            if (!result.Success) return NotFound(result.Error);
            return NoContent();
        }
    }
}

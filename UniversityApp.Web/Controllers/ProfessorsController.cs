// File: UniversityApp.Web/Controllers/ProfessorsController.cs

using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityApp.Application.DTOs;
using UniversityApp.Application.Features.Professors.Commands;
using UniversityApp.Application.Features.Professors.Queries;
using System.Threading.Tasks;

namespace UniversityApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProfessorsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllProfessorsQuery());
            if (!result.Success) return BadRequest(result.Error);
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetProfessorByIdQuery { ProfessorId = id });
            if (!result.Success) return NotFound(result.Error);
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProfessorDto dto)
        {
            var cmd = new CreateProfessorCommand { Professor = dto };
            var result = await _mediator.Send(cmd);
            if (!result.Success) return BadRequest(result.Error);
            return CreatedAtAction(nameof(Get), new { id = result.Data.ProfessorId }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateProfessorDto dto)
        {
            var cmd = new UpdateProfessorCommand { ProfessorId = id, Updated = dto };
            var result = await _mediator.Send(cmd);
            if (!result.Success) return NotFound(result.Error);
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProfessorCommand { ProfessorId = id });
            if (!result.Success) return NotFound(result.Error);
            return NoContent();
        }
    }
}

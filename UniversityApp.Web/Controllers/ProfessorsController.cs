
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using UniversityApp.Application.DTOs;
using UniversityApp.Application.Features.Professors.Commands;
using UniversityApp.Application.Features.Professors.Queries;

namespace UniversityApp.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfessorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ✅ UPDATED: Supports pageNumber and pageSize via query string
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllProfessorsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProfessorByIdQuery(id));
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProfessorDto dto)
        {
            var result = await _mediator.Send(new CreateProfessorCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProfessorDto dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch.");
            var result = await _mediator.Send(new UpdateProfessorCommand(dto));
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProfessorCommand(id));
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}

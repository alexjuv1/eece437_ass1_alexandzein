// File: UniversityApp.Web/Controllers/DepartmentsController.cs

using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniversityApp.Application.DTOs;
using UniversityApp.Application.Features.Departments.Commands;
using UniversityApp.Application.Features.Departments.Queries;
using System.Threading.Tasks;

namespace UniversityApp.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DepartmentsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllDepartmentsQuery());
            if (!result.Success) return BadRequest(result.Error);
            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetDepartmentByIdQuery { DepartmentId = id });
            if (!result.Success) return NotFound(result.Error);
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentDto dto)
        {
            var cmd = new CreateDepartmentCommand { Department = dto };
            var result = await _mediator.Send(cmd);
            if (!result.Success) return BadRequest(result.Error);
            return CreatedAtAction(nameof(Get), new { id = result.Data.DepartmentId }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateDepartmentDto dto)
        {
            var cmd = new UpdateDepartmentCommand { DepartmentId = id, Updated = dto };
            var result = await _mediator.Send(cmd);
            if (!result.Success) return NotFound(result.Error);
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteDepartmentCommand { DepartmentId = id });
            if (!result.Success) return NotFound(result.Error);
            return NoContent();
        }
    }
}

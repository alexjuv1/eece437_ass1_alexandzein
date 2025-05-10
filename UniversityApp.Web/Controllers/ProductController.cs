using Microsoft.AspNetCore.Mvc;
using MediatR;
using UniversityApp.Application.Features.Products.Commands;
using UniversityApp.Application.DTOs;
using System.Threading.Tasks;

namespace UniversityApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            var command = new CreateProductCommand(dto);
            var id = await _mediator.Send(command);
            return RedirectToAction("Details", new { id });
        }

        // Add GET view etc. later
    }
}

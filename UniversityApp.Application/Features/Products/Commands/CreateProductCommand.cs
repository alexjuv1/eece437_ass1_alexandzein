using MediatR;
using UniversityApp.Application.DTOs;

namespace UniversityApp.Application.Features.Products.Commands
{
    public class CreateProductCommand : IRequest<int>  // returns ProductId
    {
        public CreateProductDto ProductDto { get; set; }

        public CreateProductCommand(CreateProductDto productDto)
        {
            ProductDto = productDto;
        }
    }
}

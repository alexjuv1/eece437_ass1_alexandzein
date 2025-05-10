using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UniversityApp.Application.Features.Products.Commands;
using UniversityApp.Core.Entities;
using UniversityApp.Core.Interfaces;
using AutoMapper;

namespace UniversityApp.Application.Features.Products.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public CreateProductHandler(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request.ProductDto);
            await _repo.AddAsync(product);
            return product.Id;
        }
    }
}

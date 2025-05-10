using AutoMapper;
using UniversityApp.Application.DTOs;
using UniversityApp.Core.Entities;

namespace UniversityApp.Web.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}

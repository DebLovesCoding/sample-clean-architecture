using AutoMapper;
using Intelligent.Solution.Infrastructure.Dtos;
using Intelligent.Solution.Domain.Entities;

namespace Intelligent.Solution.Infrastructure.MappingProfiles
{
    public class ProductMap : Profile
    {
        public ProductMap()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}

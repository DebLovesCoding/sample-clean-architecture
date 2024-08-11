using AutoMapper;
using Intelligent.Solution.Infrastructure.Dtos;
using Intelligent.Solution.Domain.Entities;

namespace Intelligent.Solution.Infrastructure.MappingProfiles
{
    public class ProductCategoryMap : Profile
    {
        public ProductCategoryMap()
        {
            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
        }
    }
}

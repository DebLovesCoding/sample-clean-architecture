using Intelligent.Solution.Common;
using Intelligent.Solution.Infrastructure.Dtos;
using MediatR;

namespace Intelligent.Solution.Infrastructure.Commands
{
    public class ProductCategoryModifyRequest : IRequest<BaseResponse<ProductCategoryDto>>
    {
        public ProductCategoryDto? ProductCategory { get; set; }
    }
}

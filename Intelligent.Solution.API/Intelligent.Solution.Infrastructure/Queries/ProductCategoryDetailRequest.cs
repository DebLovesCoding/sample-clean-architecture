using Intelligent.Solution.Common;
using Intelligent.Solution.Infrastructure.Dtos;
using MediatR;

namespace Intelligent.Solution.Infrastructure.Queries
{
    public class ProductCategoryDetailRequest : IRequest<BaseResponse<ProductCategoryDto>>
    {
        public int Id { get; set; }
    }
}

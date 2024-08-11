using Intelligent.Solution.Common;
using Intelligent.Solution.Infrastructure.Dtos;
using MediatR;

namespace Intelligent.Solution.Infrastructure.Queries
{
    public class ProductDetailRequest : IRequest<BaseResponse<ProductDto>>
    {
        public int Id { get; set; }
    }
}

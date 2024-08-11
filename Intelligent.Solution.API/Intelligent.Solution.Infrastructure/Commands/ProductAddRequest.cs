using Intelligent.Solution.Common;
using Intelligent.Solution.Infrastructure.Dtos;
using MediatR;

namespace Intelligent.Solution.Infrastructure.Commands
{
    public class ProductAddRequest : IRequest<BaseResponse<ProductDto>>
    {
        public ProductDto? Product { get; set; }
    }
}

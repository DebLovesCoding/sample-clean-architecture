using Intelligent.Solution.Common;
using Intelligent.Solution.Infrastructure.Dtos;

namespace Intelligent.Solution.Infrastructure.Responses
{
    public class ProductDetailResponse<T> : BaseResponse<T> where T : ProductDto
    {
    }
}

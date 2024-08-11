using Intelligent.Solution.Infrastructure.Responses;
using MediatR;

namespace Intelligent.Solution.Infrastructure.Commands
{
    public class ProductCategoryDeleteRequest : IRequest<EntityDeleteResponse>
    {
        public int Id { get; set; }
    }
}

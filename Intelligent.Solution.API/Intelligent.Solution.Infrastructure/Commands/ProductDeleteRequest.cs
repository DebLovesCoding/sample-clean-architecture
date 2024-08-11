using Intelligent.Solution.Infrastructure.Responses;
using MediatR;

namespace Intelligent.Solution.Infrastructure.Commands
{
    public class ProductDeleteRequest : IRequest<EntityDeleteResponse>
    {
        public int Id { get; set; }
    }
}

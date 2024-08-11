using Intelligent.Solution.Infrastructure.Commands;
using Intelligent.Solution.Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Intelligent.Solution.API.Controllers
{
    /// <summary>
    /// Defines the <see cref="ProductController"/>
    /// </summary>
    /// <remarks>
    /// Defines the constructor for <see cref="ProductController"/>
    /// </remarks>
    /// <param name="mediator"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Gets the details of a Product by Id
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{productId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetProductByAsync([FromRoute] int productId, CancellationToken cancellationToken)
        {
            var request = new ProductDetailRequest { Id = productId };
            var response = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccess)
            {
                return new OkObjectResult(response);
            }
            else
            {
                return new NotFoundObjectResult(response);
            }
        }

        /// <summary>
        /// Adds a new Product
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("save")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddProductAsync([FromBody] ProductAddRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccess)
            {
                return new OkObjectResult(response);
            }
            else
            {
                return new BadRequestObjectResult(response);
            }
        }

        /// <summary>
        /// Updates an existing Product
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProductAsync([FromBody] ProductModifyRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccess)
            {
                return new OkObjectResult(response);
            }
            else
            {
                return new BadRequestObjectResult(response);
            }
        }

        /// <summary>
        /// Deletes an existing Product by Id
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{productId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int productId, CancellationToken cancellationToken)
        {
            var request = new ProductDeleteRequest { Id = productId };
            var response = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);
            if (response.IsSuccess)
            {
                return new OkObjectResult(response);
            }
            else
            {
                return new BadRequestObjectResult(response);
            }
        }
    }
}

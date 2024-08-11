using Intelligent.Solution.Infrastructure.Commands;
using Intelligent.Solution.Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Intelligent.Solution.API.Controllers
{
    /// <summary>
    /// Defines the <see cref="ProductCategoryController"/>
    /// </summary>
    /// <remarks>
    /// Defines the constructor for <see cref="ProductCategoryController"/>
    /// </remarks>
    /// <param name="mediator"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Gets a Product Category By Id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{categoryId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int categoryId, CancellationToken cancellationToken)
        {
            var request = new ProductCategoryDetailRequest { Id = categoryId };
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
        /// Adds a new entry in Product Category
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("save")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddProductCategoryAsync([FromBody] ProductCategoryAddRequest request, CancellationToken cancellationToken)
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
        /// Updates an existing Product Category
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProductCategoryAsync([FromBody] ProductCategoryModifyRequest request, CancellationToken cancellationToken)
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
        /// Deletes an existing Product Category by Id
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{categoryId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteProductCategoryAsync([FromRoute] int categoryId, CancellationToken cancellationToken)
        {
            var request = new ProductCategoryDeleteRequest { Id = categoryId };
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

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuzShop.CQ.Commands.ProductCommands;
using MuzShop.CQ.Queries.ProductQueries;
using MuzShop.CQ.Queries.RoleQueries;

namespace MuzShop.Controllers
{
    [ApiController]
    [Route("/api/Product")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<int>> AddProduct([FromBody] AddProductCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return Ok(id);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteProductCommand { ProductId = id }, cancellationToken);
            return Ok();
        }

        [Authorize(Roles = "admin, moderator")]
        [HttpPatch("[action]")]
        public async Task<ActionResult<int>> UpdateProduct(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var productId = await _mediator.Send(command, cancellationToken);
            return Ok(productId);
        }

        [Authorize(Roles = "admin, moderator, user")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProduct(Guid id, CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(new GetProductQuery { ProductId = id }, cancellationToken);
            return Ok(product);
        }

        [Authorize(Roles = "admin, moderator, user")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
        {
            var roles = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);
            return Ok(roles);
        }
    }
}

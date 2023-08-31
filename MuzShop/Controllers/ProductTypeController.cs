using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuzShop.CQ.Commands.ProductCommands;
using MuzShop.CQ.Commands.ProductTypeCommands;
using MuzShop.CQ.Queries.ProductQueries;
using MuzShop.CQ.Queries.ProductTypeQueries;

namespace MuzShop.Controllers
{
    [ApiController]
    [Route("/api/ProductType")]
    public class ProductTypeController : Controller
    {
        private readonly IMediator _mediator;

        public ProductTypeController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Guid>> AddProductType([FromBody] AddProductTypeCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return Ok(id);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductType(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteProductTypeCommand { TypeId = id }, cancellationToken);
            return Ok();
        }

        [Authorize(Roles = "admin, moderator")]
        [HttpPatch("[action]")]
        public async Task<ActionResult<int>> UpdateProductType(UpdateProductTypeCommand command, CancellationToken cancellationToken)
        {
            var productTypeId = await _mediator.Send(command, cancellationToken);
            return Ok(productTypeId);
        }

        [Authorize(Roles = "admin, moderator")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductType(Guid id, CancellationToken cancellationToken)
        {
            var productType = await _mediator.Send(new GetProductTypeQuery { ProductTypeId = id }, cancellationToken);
            return Ok(productType);
        }

        [Authorize(Roles = "admin, moderator")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProductTypes(CancellationToken cancellationToken)
        {
            var roles = await _mediator.Send(new GetAllProductTypesQuery(), cancellationToken);
            return Ok(roles);
        }
    }
}

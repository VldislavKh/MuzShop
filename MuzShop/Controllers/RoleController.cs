using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuzShop.CQ.Commands.RoleCommands;
using MuzShop.CQ.Queries.ProductQueries;
using MuzShop.CQ.Queries.RoleQueries;

namespace MuzShop.Controllers
{
    [ApiController]
    [Route("/api/Role")]
    public class RoleController : Controller
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<int>> AddRole([FromBody] AddRoleCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return Ok(id);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteRole(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteRoleCommand() { Id = id }, cancellationToken);
            return NoContent();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
        {
            var roles = await _mediator.Send(new GetAllRolesQuery(), cancellationToken);
            return Ok(roles);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetRole(Guid id, CancellationToken cancellationToken)
        {
            var role = await _mediator.Send(new GetRoleQuery { RoleId = id }, cancellationToken);
            return Ok(role);
        }
    }
}

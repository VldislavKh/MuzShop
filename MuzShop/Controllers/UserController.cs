using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuzShop.CQ.Commands.UserCommands;
using MuzShop.CQ.Queries.UserQueries;

namespace MuzShop.Controllers
{
    [ApiController]
    [Route("/api/User")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<int>> AddUser(AddUserCommand command, CancellationToken cancellationToken)
        {
            var id =  await _mediator.Send(command, cancellationToken);
            return Ok(id);
        }


        [Authorize(Roles = "admin")]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteUserCommand() { UserId = id }, cancellationToken);
            return NoContent();
        }

        [HttpPatch("[action]")]
        public async Task<ActionResult<Guid>> UpdateUser(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return Ok(id);
        }


        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
        {
            var users = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);
            return Ok(users);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetUser(Guid id, CancellationToken cancellationToken)
        {
            var user = await _mediator.Send(new GetUserQuery { UserId = id }, cancellationToken);
            return Ok(user);
        }
    }
}

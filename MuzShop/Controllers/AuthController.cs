using MediatR;
using Microsoft.AspNetCore.Mvc;
using MuzShop.CQ.Commands.UserCommands;
using MuzShop.CQ.Queries.UserQueries;
using MuzShop.Request;

namespace MuzShop.Controllers
{
    [ApiController]
    [Route("/api/Auth")]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _mediator.Send(new AuthenticateUserQuery
            { Username = request.Name, Password = request.Password });

            if (user != null)
            {
                //Generate JWT
                return Ok(await _mediator
                    .Send(new GenerateJwtCommand { 
                        User = user }));
            }

            return Unauthorized();
        }
    }
}

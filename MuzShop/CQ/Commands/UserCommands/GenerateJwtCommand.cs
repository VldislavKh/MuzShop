using Domain.Entities;
using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Commands.UserCommands
{
    public class GenerateJwtCommand : IRequest<string>
    {
        public User User { get; set; }

        public class GenerateJwtCommandHandler : IRequestHandler<GenerateJwtCommand, string>
        {
            private readonly IAuthService _service;

            public GenerateJwtCommandHandler(IAuthService service)
            {
                _service = service;
            }

            public async Task<string> Handle(GenerateJwtCommand command, CancellationToken cancellationToken)
            {
                return _service.GenerateJWT(command.User);
            }
        }
    }
}

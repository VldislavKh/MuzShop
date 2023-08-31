using Domain.Entities;
using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Queries.UserQueries
{
    public class AuthenticateUserQuery : IRequest<User>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, User>
        {
            private readonly IAuthService _service;

            public AuthenticateUserQueryHandler(IAuthService service)
            {
                _service = service;
            }

            public async Task<User> Handle(AuthenticateUserQuery query, CancellationToken cancellationToken)
            {
                return await _service.AuthenticateAsync(query.Username, query.Password);
            }
        }
    }
}

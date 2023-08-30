using Domain.Entities;
using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Queries.UserQueries
{
    public class GetUserQuery : IRequest<User>
    {
        public Guid UserId { get; set; }

        public class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
        {
            private readonly IUserService _userService;

            public GetUserQueryHandler(IUserService userService)
            {
                _userService = userService;
            }

            public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                return await _userService.GetAsync(request.UserId);
            }
        }
    }
}

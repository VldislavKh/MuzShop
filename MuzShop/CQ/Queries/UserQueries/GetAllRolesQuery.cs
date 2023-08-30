using Domain.Entities;
using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Queries.UserQueries
{
    public class GetAllUsersQuery : IRequest<List<User>>
    {
        public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
        {
            private readonly IUserService _usersInfo;

            public GetAllUsersQueryHandler(IUserService usersInfo)
            {
                _usersInfo = usersInfo;
            }

            public async Task<List<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
            {
                return await _usersInfo.GetAllAsync();
            }
        }
    }
}

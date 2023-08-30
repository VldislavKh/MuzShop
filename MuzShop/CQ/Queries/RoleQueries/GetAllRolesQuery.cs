using Domain.Entities;
using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Queries.RoleQueries
{
    public class GetAllRolesQuery : IRequest<List<Role>>
    {
        public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<Role>>
        {
            private readonly IRoleService _rolesInfo;

            public GetAllRolesQueryHandler(IRoleService rolesInfo)
            {
                _rolesInfo = rolesInfo;
            }

            public async Task<List<Role>> Handle(GetAllRolesQuery query, CancellationToken cancellationToken)
            {
                return await _rolesInfo.GetAllAsync();
            }
        }
    }
}

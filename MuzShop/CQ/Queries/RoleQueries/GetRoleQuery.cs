using Domain.Entities;
using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Queries.RoleQueries
{
    public class GetRoleQuery : IRequest<Role>
    {
        public Guid RoleId { get; set; }

        public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, Role>
        {
            private readonly IRoleService _roleService;

            public GetRoleQueryHandler(IRoleService roleService)
            {
                _roleService = roleService; 
            }

            public async Task<Role> Handle(GetRoleQuery request, CancellationToken cancellationToken)
            {
                return await _roleService.GetAsync(request.RoleId);
            }
        }
    }
}

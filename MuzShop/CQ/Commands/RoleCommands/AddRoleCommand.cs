using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Commands.RoleCommands
{
    public class AddRoleCommand : IRequest<Guid>
    {
        public string Name { get; set; }

        public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, Guid>
        {
            private readonly IRoleService _addRole;

            public AddRoleCommandHandler(IRoleService addRole)
            {
                _addRole = addRole;
            }

            public async Task<Guid> Handle(AddRoleCommand command, CancellationToken cancellationToken)
            {
                return await _addRole.AddAsync(command.Name);
            }
        }
    }
}

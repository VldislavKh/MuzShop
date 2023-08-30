using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Commands.RoleCommands
{
    public class DeleteRoleCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Unit>
        {
            private readonly IRoleService _deleteRole;

            public DeleteRoleCommandHandler(IRoleService deleteRole)
            {
                _deleteRole = deleteRole;
            }

            public async Task<Unit> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
            {
                await _deleteRole.DeleteAsync(command.Id);
                return Unit.Value;
            }
        }
    }
}

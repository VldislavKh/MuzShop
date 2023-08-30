using FluentValidation;
using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;
using MuzShop.Validation.UserValidators;

namespace MuzShop.CQ.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }

        public class UpdateUserCommandHandler :IRequestHandler<UpdateUserCommand, Guid> 
        {
            private readonly IUserService _userService;
            private readonly UpdateUserCommandValidator _validator;

            public UpdateUserCommandHandler(IUserService userService)
            {
                _userService = userService;
                _validator = new UpdateUserCommandValidator();
            }

            public async Task<Guid> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
            {
                await _validator.ValidateAndThrowAsync(command);

                return await _userService.UpdateAsync(command.UserId, command.Name, command.Password, command.RoleId);
            }
        }
    }
}

using FluentValidation;
using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;
using MuzShop.Validation.UserValidators;

namespace MuzShop.CQ.Commands.UserCommands
{
    public class AddUserCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }

        public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Guid> 
        {
            private readonly IUserService _userService;
            private readonly AddUserCommandValidator _validator;

            public AddUserCommandHandler(IUserService userService)
            {
                _userService = userService; 
                _validator = new AddUserCommandValidator();
            }

            public async Task<Guid> Handle(AddUserCommand command, CancellationToken cancellationToken)
            {
                await _validator.ValidateAndThrowAsync(command, cancellationToken);
                return await _userService.AddAsync(command.Name, command.Password, command.RoleId);
            }
        }
    }
}

using FluentValidation;
using MuzShop.CQ.Commands.UserCommands;

namespace MuzShop.Validation.UserValidators
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().WithMessage("Имя не может быть пустым!");

            RuleFor(command => command.Password).NotEmpty().WithMessage("Пароль не может быть пустым!");
        }
    }
}

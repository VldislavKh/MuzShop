using FluentValidation;
using MuzShop.CQ.Commands.ProductCommands;

namespace MuzShop.Validation.ProductValidators
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(command => command.Price).GreaterThan(0)
                .WithMessage("Значение цены не может быть 0!");

            RuleFor(command => command.Amount).GreaterThanOrEqualTo(0)
                .WithMessage("Наличие должно быть положительным!");
        }
    }
}

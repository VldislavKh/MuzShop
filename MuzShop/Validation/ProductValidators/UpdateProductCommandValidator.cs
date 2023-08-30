using FluentValidation;
using MuzShop.CQ.Commands.ProductCommands;

namespace MuzShop.Validation.ProductValidators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.Price).GreaterThan(0)
                .WithMessage("Значение цены не может быть 0!");

            RuleFor(command => command.Amount).GreaterThanOrEqualTo(0)
                .WithMessage("Наличие должно быть положительным!");
        }
    }
}

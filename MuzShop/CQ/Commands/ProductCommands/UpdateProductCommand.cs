using FluentValidation;
using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;
using MuzShop.Validation.ProductValidators;

namespace MuzShop.CQ.Commands.ProductCommands
{
    public class UpdateProductCommand : IRequest<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid TypeId { get; set; }
        public string Name { get; set; }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
        {
            private readonly IProductService _updateProduct;
            private readonly UpdateProductCommandValidator _validator;

            public UpdateProductCommandHandler(IProductService updateProduct)
            {
                _updateProduct = updateProduct;
                _validator = new UpdateProductCommandValidator();
            }

            public async Task<Guid> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                await _validator.ValidateAndThrowAsync(command);

                return await _updateProduct.UpdateAsync(command.ProductId, command.TypeId, command.Name, command.Vendor,
                    command.Model, command.Description, command.Price, command.Amount);
            }
        }
    }
}

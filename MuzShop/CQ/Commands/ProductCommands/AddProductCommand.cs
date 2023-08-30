using Domain.Entities;
using FluentValidation;
using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;
using MuzShop.Validation.ProductValidators;

namespace MuzShop.CQ.Commands.ProductCommands
{
    public class AddProductCommand : IRequest<Guid>
    {
        public Guid TypeId { get; set; }
        public string Name { get; set; }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }

        public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
        {
            private readonly IProductService _addProduct;
            private readonly AddProductCommandValidator _validator;

            public AddProductCommandHandler(IProductService addProduct)
            {
                _addProduct = addProduct;
                _validator = new AddProductCommandValidator();
            }

            public async Task<Guid> Handle(AddProductCommand command, CancellationToken cancellationToken)
            {
                await _validator.ValidateAndThrowAsync(command);

                return await _addProduct.AddAsync(command.TypeId, command.Name, command.Vendor,
                    command.Model, command.Description, command.Price, command.Amount);
            }
        }
    }
}

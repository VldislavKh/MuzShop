using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Commands.ProductTypeCommands
{
    public class UpdateProductTypeCommand : IRequest<Guid>
    {
        public Guid ProductTypeId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public class UpdateProductTypeCommandHandler : IRequestHandler<UpdateProductTypeCommand, Guid>
        {
            private readonly IProductTypeService _updateProduct;

            public UpdateProductTypeCommandHandler(IProductTypeService updateProduct)
            {
                _updateProduct = updateProduct; 
            }

            public async Task<Guid> Handle(UpdateProductTypeCommand command, CancellationToken cancellationToken)
            {
                return await _updateProduct.UpdateAsync(command.ProductTypeId, command.Type, command.Description);
            }
        }
    }
}

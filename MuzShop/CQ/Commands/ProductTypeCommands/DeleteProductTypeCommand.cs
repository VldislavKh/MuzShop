using MediatR;
using MuzShop.CQ.Commands.ProductCommands;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Commands.ProductTypeCommands
{
    public class DeleteProductTypeCommand : IRequest<Unit>
    {
        public Guid TypeId { get; set; }

        public class DeleteProductTypeCommandHandler : IRequestHandler<DeleteProductTypeCommand, Unit>
        {
            private readonly IProductTypeService _deleteProductType;

            public DeleteProductTypeCommandHandler(IProductTypeService deleteProductType)
            {
                _deleteProductType = deleteProductType;
            }

            public async Task<Unit> Handle(DeleteProductTypeCommand command, CancellationToken cancellationToken)
            {
                await _deleteProductType.DeleteAsync(command.TypeId);
                return Unit.Value;
            }
        }
    }
}

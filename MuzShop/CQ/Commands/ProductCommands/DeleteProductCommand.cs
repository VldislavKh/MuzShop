using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Commands.ProductCommands
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public Guid ProductId { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
        {
            private readonly IProductService _deleteProduct;

            public DeleteProductCommandHandler(IProductService deleteProduct)
            {
                _deleteProduct = deleteProduct;
            }

            public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
            {
                await _deleteProduct.DeleteAsync(command.ProductId);
                return Unit.Value;
            }
        }
    }
}

using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Commands.ProductCommands
{
    public class AddProductToShoppingBasketCommand : IRequest<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }

        public class AddProductToShoppingBasketCommandHandler : IRequestHandler<AddProductToShoppingBasketCommand, Guid>
        {
            private readonly IProductService _service;
            public AddProductToShoppingBasketCommandHandler(IProductService service)
            {
                _service = service;
            }

            public async Task<Guid> Handle(AddProductToShoppingBasketCommand command, CancellationToken cancellationToken)
            {
                return await _service.AddToShoppingBasket(command.ProductId, command.UserId);
            }
        }
    }
}

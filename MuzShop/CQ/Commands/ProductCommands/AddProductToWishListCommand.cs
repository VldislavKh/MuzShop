using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Commands.ProductCommands
{
    public class AddProductToWishListCommand : IRequest<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }

        public class AddProductToWishListCommandHandler : IRequestHandler<AddProductToWishListCommand, Guid>
        {
            private readonly IProductService _service;
            public AddProductToWishListCommandHandler(IProductService service)
            {
                _service = service;
            }

            public async Task<Guid> Handle(AddProductToWishListCommand command, CancellationToken cancellationToken)
            {
                return await _service.AddToWishList(command.ProductId, command.UserId);
            }
        }
    }
}

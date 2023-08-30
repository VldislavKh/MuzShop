using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace MuzShop.CQ.Commands.ProductTypeCommands
{
    public class AddProductTypeCommand : IRequest<Guid>
    {
        public string Type { get; set; }
        public string Description { get; set; }

        public class AddProductTypeCommandHandler : IRequestHandler<AddProductTypeCommand, Guid>
        {
            private readonly IProductTypeService _addProductService;

            public AddProductTypeCommandHandler(IProductTypeService addProductService)
            {
                _addProductService = addProductService;
            }

            public async Task<Guid> Handle(AddProductTypeCommand command, CancellationToken cancellationToken)
            {
                return await _addProductService.AddAsync(command.Type, command.Description);
            }
        }
    }
}

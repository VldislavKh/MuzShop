using Domain.Entities;
using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Queries.ProductTypeQueries
{
    public class GetProductTypeQuery : IRequest<ProductType>
    {
        public Guid ProductTypeId { get; set; }

        public class GetProductTypeQuerieHandler : IRequestHandler<GetProductTypeQuery, ProductType>
        {
            private readonly IProductTypeService _getProductType;

            public GetProductTypeQuerieHandler(IProductTypeService getProductType)
            {
                _getProductType = getProductType;
            }

            public async Task<ProductType> Handle(GetProductTypeQuery command, CancellationToken cancellationToken)
            {
                return await _getProductType.GetAsync(command.ProductTypeId);
            }
        }
    }
}

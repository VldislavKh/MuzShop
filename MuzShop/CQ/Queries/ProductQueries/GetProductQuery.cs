using Domain.Entities;
using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Queries.ProductQueries
{
    public class GetProductQuery : IRequest<Product>
    {
        public Guid ProductId { get; set; }

        public class GetProductQuerieHandler : IRequestHandler<GetProductQuery, Product> 
        { 
            private readonly IProductService _getProduct;

            public GetProductQuerieHandler(IProductService getProduct)
            {
                _getProduct = getProduct;
            }

            public async Task<Product> Handle(GetProductQuery query, CancellationToken cancellationToken)
            {
                return await _getProduct.GetAsync(query.ProductId);
            }
        }
    }
}

using Domain.Entities;
using MediatR;
using MuzShop.CQ.Queries.RoleQueries;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Queries.ProductQueries
{
    public class GetAllProductsQuery : IRequest<List<Product>>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
        {
            private readonly IProductService _getAllProducts;

            public GetAllProductsQueryHandler(IProductService getAllProducts)
            {
                _getAllProducts = getAllProducts;
            }

            public async Task<List<Product>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
            {
                return await _getAllProducts.GetAllAsync();
            }
        }
    }
}

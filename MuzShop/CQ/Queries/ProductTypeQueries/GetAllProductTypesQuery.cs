using Domain.Entities;
using MediatR;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.CQ.Queries.ProductTypeQueries
{
    public class GetAllProductTypesQuery : IRequest<List<ProductType>>
    {
        public class GetAllProductTypesQueryHandler : IRequestHandler<GetAllProductTypesQuery, List<ProductType>>
        {
            private readonly IProductTypeService _getAllProductTypes;

            public GetAllProductTypesQueryHandler(IProductTypeService getAllProductTypes)
            {
                _getAllProductTypes = getAllProductTypes;
            }

            public async Task<List<ProductType>> Handle(GetAllProductTypesQuery query, CancellationToken cancellationToken)
            {
                return await _getAllProductTypes.GetAllAsync();
            }
        }
    }
}

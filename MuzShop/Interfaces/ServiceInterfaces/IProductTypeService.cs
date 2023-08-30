using Domain.Entities;

namespace MuzShop.Interfaces.ServiceInterfaces
{
    public interface IProductTypeService
    {
        public Task<Guid> AddAsync(string type, string description);

        public Task DeleteAsync(Guid typeId);

        public Task<Guid> UpdateAsync(Guid typeId, string type, string description);

        public Task<ProductType> GetAsync(Guid productTypeId);

        public Task<List<ProductType>> GetAllAsync();
    }
}

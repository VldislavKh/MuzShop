using Domain.Entities;

namespace MuzShop.Interfaces.ServiceInterfaces
{
    public interface IProductService
    {
        public Task<Guid> AddAsync(Guid typeId, string name, string vendor, string model, string description, decimal price, int amount);

        public Task DeleteAsync(Guid productId);

        public Task<Guid> UpdateAsync(Guid productId, Guid typeId, string name, string vendor, string model, string description, decimal price, int amount);

        public Task<Product> GetAsync(Guid productId);

        public Task<List<Product>> GetAllAsync();
    }
}

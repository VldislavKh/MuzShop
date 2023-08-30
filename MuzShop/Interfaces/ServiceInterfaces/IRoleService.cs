using Domain.Entities;

namespace MuzShop.Interfaces.ServiceInterfaces
{
    public interface IRoleService
    {
        public Task<Guid> AddAsync(string name);

        public Task DeleteAsync(Guid id);

        public Task<List<Role>> GetAllAsync();

        public Task<Role> GetAsync(Guid id);
    }
}

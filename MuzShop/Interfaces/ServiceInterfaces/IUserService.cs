using Domain.Entities;

namespace MuzShop.Interfaces.ServiceInterfaces
{
    public interface IUserService
    {
        public Task<Guid> AddAsync(string name, string password, Guid roleId);

        public Task DeleteAsync(Guid userId);

        public Task<Guid> UpdateAsync(Guid userId, string name, string password, Guid roleId);

        public Task<User> GetAsync(Guid userId);

        public Task<List<User>> GetAllAsync();

    }
}

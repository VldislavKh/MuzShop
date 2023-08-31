using Domain.Entities;

namespace MuzShop.Interfaces.ServiceInterfaces
{
    public interface IAuthService
    {
        public Task<User> AuthenticateAsync(string username, string password);

        public string GenerateJWT(User user);
    }
}

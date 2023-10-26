using Domain.Entities;
using Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MuzShop.ExceptionMiddleware.CustomExceptions;
using MuzShop.Interfaces.ServiceInterfaces;
using System.Security.Cryptography;
using System.Text;

namespace MuzShop.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        private readonly IMemoryCache _cache;

        public UserService(ApplicationContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Guid> AddAsync(string name, string password, Guid roleId)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == roleId)
                ?? throw new NotFoundException(nameof(_context.Roles), roleId);

            var user = new User();
            user.Name = name;
            user.Password = CreateSHA256(password);
            user.RoleId = roleId;
            user.Role = role;
            user.WishList = new List<Product>();
            user.ShoppingBasket = new List<Product>();

            await _context.AddAsync(user);
            int n = await _context.SaveChangesAsync();

            if (n > 0)
            {
                _cache.Set(user.Id, user, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
            return user.Id;
        }

        public async Task DeleteAsync(Guid userId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId)
               ?? throw new NotFoundException(nameof(_context.Users), userId);

            _context.Remove(user);
            _cache.Remove(user.Id);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> UpdateAsync(Guid userId, string name, string password, Guid roleId)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId)
               ?? throw new NotFoundException(nameof(_context.Users), userId);

            var role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == roleId)
                ?? throw new NotFoundException(nameof(_context.Roles), roleId);

            user.Name = name;
            user.Password = CreateSHA256(password);
            user.RoleId = roleId;
            user.Role = role;
            

            _context.Update(user);
            int n = await _context.SaveChangesAsync();

            if (n > 0)
            {
                _cache.Set(user.Id, user, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
            return user.Id;
        }

        public async Task<User> GetAsync(Guid userId)
        {
            User user = null;
            if (!_cache.TryGetValue(userId, out user))
            {
                user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userId)
                    ?? throw new NotFoundException(nameof(_context.Users), userId);

                _cache.Set(user.Id, user, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }

            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        private string CreateSHA256(string input)
        {
            using SHA256 hash = SHA256.Create();
            return Convert.ToHexString(hash.ComputeHash(Encoding.ASCII.GetBytes(input)));
        }
    }
}

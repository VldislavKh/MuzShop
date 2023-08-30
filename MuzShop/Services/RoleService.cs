using Domain.Infrastructure;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MuzShop.ExceptionMiddleware.CustomExceptions;
using MuzShop.Interfaces.ServiceInterfaces;
using Microsoft.Extensions.Caching.Memory;

namespace MuzShop.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationContext _context;
        private readonly IMemoryCache _cache;
        public RoleService(ApplicationContext context, IMemoryCache cache) 
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Guid> AddAsync(string name)
        {
            var role = new Role();
            role.Name = name;

            await _context.AddAsync(role);
            int n = await _context.SaveChangesAsync();

            if (n > 0)
            {
                _cache.Set(role.Id, role, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }

            return role.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == id)
                ?? throw new NotFoundException(nameof(_context.Roles), id);

            _context.Remove(role);
            _cache.Remove(role.Id);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetAsync(Guid id)
        {
            Role role = null;
            if (!_cache.TryGetValue(id, out role))
            {
                role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == id)
                    ?? throw new NotFoundException(nameof(_context.Roles), id);

                _cache.Set(role.Id, role, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }

            return role;
        }
    }
}

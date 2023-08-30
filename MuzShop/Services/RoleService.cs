using Domain.Infrastructure;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MuzShop.ExceptionMiddleware.CustomExceptions;
using MuzShop.Interfaces.ServiceInterfaces;

namespace MuzShop.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationContext _context;
        public RoleService(ApplicationContext context) 
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(string name)
        {
            var role = new Role();
            role.Name = name;

            await _context.AddAsync(role);
            await _context.SaveChangesAsync();

            return role.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == id)
                ?? throw new NotFoundException(nameof(_context.Roles), id);

            _context.Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetAsync(Guid id)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(r => r.Id == id)
                ?? throw new NotFoundException(nameof(_context.Roles), id);

            return role;
        }
    }
}

using Domain.Entities;
using Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MuzShop.ExceptionMiddleware.CustomExceptions;
using MuzShop.Interfaces.ServiceInterfaces;
using System;

namespace MuzShop.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly ApplicationContext _context;
        private readonly IMemoryCache _cache;


        public ProductTypeService(ApplicationContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }
        public async Task<Guid> AddAsync(string type, string description)
        {
            var productType = new ProductType();

            productType.Type = type;
            productType.Description = description;

            await _context.AddAsync(productType);

            int n = await _context.SaveChangesAsync();

            if (n > 0)
            {
                _cache.Set(productType.Id, productType, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }

            return productType.Id;
        }

        public async Task DeleteAsync(Guid productTypeId)
        {
            var productType = await _context.ProductTypes.SingleOrDefaultAsync(p => p.Id == productTypeId)
                ?? throw new NotFoundException(nameof(_context.ProductTypes), productTypeId);

            _context.ProductTypes.Remove(productType);
            _cache.Remove(productType.Id);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> UpdateAsync(Guid productTypeId, string type, string description) //TODO productTypeUpdateModel
        {
            var productType = await _context.ProductTypes.SingleOrDefaultAsync(t => t.Id == productTypeId)
                ?? throw new NotFoundException(nameof(_context.ProductTypes), productTypeId);

            productType.Type = type;
            productType.Description = description;

            _context.Update(productType);

            int n = await _context.SaveChangesAsync();
            if (n > 0)
            {
                _cache.Set(productType.Id, productType, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
            return productType.Id;
        }

        public async Task<ProductType> GetAsync(Guid productTypeId)
        {
            ProductType productType = null;

            if (!_cache.TryGetValue(productTypeId, out productType))
            {
                productType = await _context.ProductTypes.SingleOrDefaultAsync(p => p.Id == productTypeId)
                    ?? throw new NotFoundException(nameof(_context.ProductTypes), productTypeId);

                _cache.Set(productType.Id, productType, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });
            }
            return productType;
        }

        public async Task<List<ProductType>> GetAllAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}

﻿using Domain.Entities;
using Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MuzShop.ExceptionMiddleware.CustomExceptions;
using MuzShop.Interfaces.ServiceInterfaces;
using System;
using System.Runtime.CompilerServices;

namespace MuzShop.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationContext _context;
        //private readonly IMemoryCache _cache;

        public ProductService(ApplicationContext context, IMemoryCache cache)
        {
            _context = context;
            //_cache = cache;
        }

        public async Task<Guid> AddAsync(Guid typeId, string name, string vendor, string model, string description, decimal price, int amount)
        {
            var product = new Product();
            var type = await _context.ProductTypes.SingleOrDefaultAsync(t => t.Id == typeId)
                ?? throw new NotFoundException(nameof(_context.ProductTypes), typeId);

            product.TypeId = typeId;
            product.Type = type;
            product.Name = name;
            product.Vendor = vendor;
            product.Model = model;
            product.Description = description;
            product.Price = price;
            product.Amount = amount;

            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task DeleteAsync(Guid productId)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == productId)
                ?? throw new NotFoundException(nameof(_context.Products), productId);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> UpdateAsync(Guid productId, Guid typeId, string name, string vendor, string model, string description, decimal price, int amount)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == productId)
                ?? throw new NotFoundException(nameof(_context.Products), productId);

            var type = await _context.ProductTypes.SingleOrDefaultAsync(t => t.Id == typeId)
                ?? throw new NotFoundException(nameof(_context.ProductTypes), typeId);

            product.TypeId = typeId;
            product.Type = type;
            product.Name = name;
            product.Vendor = vendor;
            product.Model = model;
            product.Description = description;
            product.Price = price;
            product.Amount = amount;

            _context.Update(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<Product> GetAsync(Guid productId)
        {
            var product = await _context.Products.SingleOrDefaultAsync(p => p.Id == productId)
                ?? throw new NotFoundException(nameof(_context.Products), productId);

            product.Type = await _context.ProductTypes.SingleOrDefaultAsync(type => type.Id == product.TypeId);

            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}

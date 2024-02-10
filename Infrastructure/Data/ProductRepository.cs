﻿
using Core.Interfaces;
using ECommerceAPI.Data;
using ECommerceAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
                _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetAllProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
         return await  _context.Products
                .Include(p=>p.ProductType)
                .Include(p=>p.ProductBrand)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetAllProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public async Task<Product> GetProductbyIdAsync(int id)
        {
            return await _context.Products
                 .Include(p => p.ProductType)
                .Include(p => p.ProductBrand)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}

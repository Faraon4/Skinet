using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
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

            // Get the BRANDS
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
           return await _context.ProductBrands.ToListAsync();
        }

            // Get the Products
         public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {

                // By adding .Include() we are adding EagerLoading
                // EagerLoading means that we want to add the ProductType and ProductBrand in pur request
                // So , in Postman, when we will call the request , in the requested fied we will have the values of the brand and type
                // Before this , it was just null

            return await _context.Products
                                 .Include(p => p.ProductType)
                                 .Include(p => p.ProductBrand)
                                 .ToListAsync();
            
        }
            // Get the TYPES
        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
             return await _context.ProductTypes.ToListAsync();
        }

        // Get Product By ID
        public async Task<Product> GetProductByIdAsync(int id)
        {

            // Here we add EagerLoading as well
            // The only problem here is that we need to use FirstOrDefaultAsync or SingleOrDefaultAsync

            return await _context.Products
                                 .Include(p => p.ProductType)
                                 .Include(p => p.ProductBrand)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }

            // GetBrand By ID
        public async Task<ProductBrand> GetProductBrandByIdAsync(int id)
        {
             return await _context.ProductBrands.FindAsync(id);
        }

            // Get Type By ID
        public async Task<ProductType> GetProductTypeByIdAsync(int id)
        {
             return await _context.ProductTypes.FindAsync(id);
        }

    }
}

       
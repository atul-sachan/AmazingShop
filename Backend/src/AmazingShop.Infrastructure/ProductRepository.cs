using System.Collections.Generic;
using System.Threading.Tasks;
using AmazingShop.Core.Entities;
using AmazingShop.Core.Interfaces;
using AmazingShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly AmazingShopContext context;
        public ProductRepository(AmazingShopContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            var productBrands = new List<ProductBrand>();
            productBrands = await this.context.ProductBrands.ToListAsync();
            return productBrands;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = new Product();
            //product = await this.context.Products.FindAsync(id);
            product = await this.context
                .Products
                .Include(x=> x.ProductBrand)
                .Include(x=> x.ProductType)
                .FirstOrDefaultAsync(x=>x.Id == id);
            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            // this is called Eager Loading
            // Eager Loading is default Loading
            var products = new List<Product>();
            products = await this.context
                        .Products
                        .Include(p => p.ProductBrand)
                        .Include(p => p.ProductType)
                        .ToListAsync();
            return products;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            var productTypes = new List<ProductType>();
            productTypes = await this.context.ProductTypes.ToListAsync();
            return productTypes;
        }
    }
}
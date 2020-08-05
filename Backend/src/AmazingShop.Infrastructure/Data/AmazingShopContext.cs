using System.Reflection;
using AmazingShop.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Infrastructure.Data
{
    public class AmazingShopContext : DbContext
    {
        public AmazingShopContext(DbContextOptions<AmazingShopContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
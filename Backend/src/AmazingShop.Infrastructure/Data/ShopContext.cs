using AmazingShop.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Infrastructure.Data
{
    public class AmazingShopContext : DbContext
    {
        public AmazingShopContext(DbContextOptions<AmazingShopContext> options): base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; } 
    }
}
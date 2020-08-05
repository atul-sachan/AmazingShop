using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using AmazingShop.Core.Entities;
using Microsoft.Extensions.Logging;

namespace AmazingShop.Infrastructure.Data
{
    public class AmazingShopContextSeed
    {
        public static async Task SeedAsync(AmazingShopContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var binDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                //var rootDirectory = Path.GetFullPath(Path.Combine(binDirectory, ".."));

                if (!context.ProductBrands.Any() && File.Exists(Path.Combine(binDirectory, $@"Data\SeedData\brands.json")))
                {
                    var brandsData = File.ReadAllText(Path.Combine(binDirectory, $@"Data\SeedData\brands.json"));
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var brand in brands)
                    {
                        await context.ProductBrands.AddAsync(brand);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any() && File.Exists(Path.Combine(binDirectory, $@"Data\SeedData\types.json")))
                {
                    var typesData = File.ReadAllText(Path.Combine(binDirectory, $@"Data\SeedData\types.json"));
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var item in types)
                    {
                        await context.ProductTypes.AddAsync(item);
                    }
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any() && File.Exists(Path.Combine(binDirectory, $@"Data\SeedData\products.json")))
                {
                    var productsData = File.ReadAllText(Path.Combine(binDirectory, $@"Data\SeedData\products.json"));
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var product in products)
                    {
                        await context.Products.AddAsync(product);
                    }
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<AmazingShopContextSeed>();
                logger.LogError(ex.Message, "An error occurred during Seed insert data");
            }


        }
    }
}
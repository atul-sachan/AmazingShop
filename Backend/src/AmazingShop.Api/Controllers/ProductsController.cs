using System.Linq;
using System.Threading.Tasks;
using AmazingShop.Core.Entities;
using AmazingShop.Core.Interfaces;
using AmazingShop.Core.Specification;
using AmazingShop.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> productRepo;
        private readonly IRepository<ProductBrand> brandRepo;
        private readonly IRepository<ProductType> typeRepo;
        private readonly IProductRepository repository;

        public ProductsController(
            IProductRepository repository,
            IRepository<Product> productRepo,
            IRepository<ProductBrand> brandRepo,
            IRepository<ProductType> typeRepo
        )
        {
            this.repository = repository;
            this.productRepo = productRepo;
            this.brandRepo = brandRepo;
            this.typeRepo = typeRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            //var products = await this.repository.GetProductsAsync();
            var spec = new ProductsWithTypeAndBrandSpecification();
            var products = await this.productRepo.ListAsync(spec);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            //var product = await this.repository.GetProductByIdAsync(id);
            //var spec = new ProductsWithTypeAndBrandSpecification();
            var product = await this.productRepo.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetProductBrands()
        {
            var brands = await this.brandRepo.GetListAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetProductTypes()
        {
            var types = await this.typeRepo.GetListAsync();
            return Ok(types);
        }
    }
}
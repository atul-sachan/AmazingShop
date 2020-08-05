using System.Collections.Generic;
using System.Threading.Tasks;
using AmazingShop.Api.Models;
using AmazingShop.Core.Entities;
using AmazingShop.Core.Interfaces;
using AmazingShop.Core.Specification;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AmazingShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> productRepo;
        private readonly IRepository<ProductBrand> brandRepo;
        private readonly IRepository<ProductType> typeRepo;
        private readonly IMapper mapper;
        private readonly IProductRepository repository;

        public ProductsController(
            IProductRepository repository,
            IRepository<Product> productRepo,
            IRepository<ProductBrand> brandRepo,
            IRepository<ProductType> typeRepo,
            IMapper mapper
        )
        {
            this.repository = repository;
            this.productRepo = productRepo;
            this.brandRepo = brandRepo;
            this.typeRepo = typeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            //var products = await this.repository.GetProductsAsync();
            var spec = new ProductsWithTypeAndBrandSpecification();
            var products = await this.productRepo.ListAsync(spec);
            return Ok(mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductModel>>(products));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            //var product = await this.repository.GetProductByIdAsync(id);
            var spec = new ProductsWithTypeAndBrandSpecification(id);
            var product = await this.productRepo.GetEntityWithSpec(spec);
            return Ok(mapper.Map<ProductModel>(product));
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
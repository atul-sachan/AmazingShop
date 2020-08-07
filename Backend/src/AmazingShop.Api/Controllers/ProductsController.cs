using System.Collections.Generic;
using System.Threading.Tasks;
using AmazingShop.Api.Errors;
using AmazingShop.Api.Helpers;
using AmazingShop.Api.Models;
using AmazingShop.Core.Entities;
using AmazingShop.Core.Interfaces;
using AmazingShop.Core.Specification;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AmazingShop.Api.Controllers
{
    public class ProductsController : BaseApiController
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
        public async Task<ActionResult<IReadOnlyList<ProductModel>>> GetProducts([FromQuery]ProductSpecParams productParams)
        {
            //var products = await this.repository.GetProductsAsync();
            var spec = new ProductsWithTypeAndBrandSpecification(productParams);
            var countSpec = new ProductWithFiltersForCountSpecificication(productParams);

            var totalItems = await this.productRepo.CountAsync(countSpec);
            var products = await this.productRepo.ListAsync(spec);
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductModel>>(products);
            return Ok(new Pagination<ProductModel>(productParams.PageIndex, productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductModel>> GetProduct(int id)
        {
            //var product = await this.repository.GetProductByIdAsync(id);
            var spec = new ProductsWithTypeAndBrandSpecification(id);
            var product = await this.productRepo.GetEntityWithSpec(spec);
            if (product == null)
                return new NotFoundObjectResult(new ApiResponse(404));
            return Ok(mapper.Map<ProductModel>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await this.brandRepo.GetListAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var types = await this.typeRepo.GetListAsync();
            return Ok(types);
        }
    }
}
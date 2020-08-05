using AmazingShop.Core.Entities;

namespace AmazingShop.Core.Specification
{
    public class ProductsWithTypeAndBrandSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypeAndBrandSpecification()
        {
            AddInclude(x=>x.ProductBrand);
            AddInclude(x=>x.ProductType);
        }
    }
}
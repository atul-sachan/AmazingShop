using AmazingShop.Api.Models;
using AmazingShop.Core.Entities;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace AmazingShop.Api.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductModel, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductModel destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
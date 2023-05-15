using System;
using AutoMapper;
using eCart.API.Data.DTOs;
using eCart.API.Models;
using Microsoft.Extensions.Configuration;

namespace eCart.API.Data.Helpers
{
    // Resolver for Product Picture
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }
            return null;
        }
    }
}


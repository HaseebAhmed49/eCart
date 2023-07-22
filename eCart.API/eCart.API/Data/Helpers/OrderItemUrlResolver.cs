using System;
using AutoMapper;
using eCart.API.Data.DTOs;
using eCart.API.Data.DTOs.Order;
using eCart.API.Data.Models.OrderAggregate;
using eCart.API.Models;
using Microsoft.Extensions.Configuration;

namespace eCart.API.Data.Helpers
{
    // Resolver for Product Picture
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        private readonly IConfiguration _config;

        public OrderItemUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
            {
                return _config["ApiUrl"] + source.ItemOrdered.PictureUrl;
            }
            return null;
        }
    }
}


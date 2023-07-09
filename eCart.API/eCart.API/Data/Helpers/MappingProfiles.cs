using System;
using AutoMapper;
using eCart.API.Data.DTOs;
using eCart.API.Data.DTOs.Basket;
using eCart.API.Data.DTOs.Identity;
using eCart.API.Data.Models;
using eCart.API.Data.Models.Identity;
using eCart.API.Models;

namespace eCart.API.Data.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Product, ProductToReturnDTO>()
				.ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
				.ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
				.ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

			CreateMap<Address, AddressDTO>().ReverseMap();

			CreateMap<CustomerBasketDTO, CustomerBasket>();

			CreateMap<BasketItemDTO, BasketItem>();

			CreateMap<AddressDTO, eCart.API.Data.Models.OrderAggregate.Address>();
        }
    }
}


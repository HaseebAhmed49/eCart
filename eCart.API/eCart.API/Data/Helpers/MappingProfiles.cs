using System;
using AutoMapper;
using eCart.API.Data.DTOs;
using eCart.API.Models;

namespace eCart.API.Data.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Product, ProductToReturnDTO>();
		}
	}
}


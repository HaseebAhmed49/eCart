using System;
using eCart.API.Models;

namespace eCart.API.Services.ProductService
{
	public interface IProductService
	{
		public Task AddProduct(Product product);
	}
}


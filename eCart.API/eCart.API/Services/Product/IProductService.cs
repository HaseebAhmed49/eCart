using System;
using eCart.API.Models.Product;

namespace eCart.API.Services.Product
{
	public interface IProductService
	{
		public Task AddProduct(Product product);
	}
}


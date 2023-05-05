using System;
using System.Collections.Generic;
using eCart.API.Models;

namespace eCart.API.Services.ProductService
{
	public interface IProductService
	{
		public Task<IReadOnlyList<Product>> GetProductsAsync();

		public Task<Product> GetProductByIdAsync(int id);
	}
}
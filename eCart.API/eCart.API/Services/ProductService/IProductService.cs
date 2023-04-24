﻿using System;
using eCart.API.Models;

namespace eCart.API.Services.ProductService
{
	public interface IProductService
	{
		public Task AddProduct(Product product);

		public Task GetProducts();

		public Task GetProductById(int id);
	}
}


using System;
using eCart.API.Data;
using eCart.API.Models;
using Microsoft.EntityFrameworkCore;

namespace eCart.API.Services.ProductService
{
	public class ProductService: IProductService
	{

        private readonly StoreContext _context;

		public ProductService(StoreContext context)
		{
            _context = context;
		}

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
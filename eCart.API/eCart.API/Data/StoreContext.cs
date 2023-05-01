using eCart.API.Models;
using Microsoft.EntityFrameworkCore;

namespace eCart.API.Data
{
	public class StoreContext: DbContext
	{
		public StoreContext(DbContextOptions options): base(options)
		{

		}

		public DbSet<Product> Products { get; set; }
	}
}


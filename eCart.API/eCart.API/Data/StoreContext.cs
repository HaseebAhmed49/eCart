using System.Reflection;
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

		public DbSet<ProductType> ProductTypes { get; set; }

		public DbSet<ProductBrand> ProductBrands { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
	}
}


using System;
using System.Text.Json;
using eCart.API.Models;

namespace eCart.API.Data.SeedData
{
	public class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext context)
		{
			if(!context.ProductBrands.Any())
			{
				var brandsData = File.ReadAllText("/Users/haseebahmed/Desktop/dotNet/eCart/eCart.API/eCart.API/Data/SeedData/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

				context.ProductBrands.AddRange(brands);
			}

            if (!context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("/Users/haseebahmed/Desktop/dotNet/eCart/eCart.API/eCart.API/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);
            }


            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("/Users/haseebahmed/Desktop/dotNet/eCart/eCart.API/eCart.API/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}


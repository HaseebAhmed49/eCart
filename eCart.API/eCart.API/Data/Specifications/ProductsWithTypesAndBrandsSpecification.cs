using System;
using eCart.API.Models;

namespace eCart.API.Data.Specifications
{
	public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
	{
		public ProductsWithTypesAndBrandsSpecification()
		{
			AddInclude(x => x.ProductBrand);
			AddInclude(x => x.ProductType);
		}

		public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
		{
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }
}


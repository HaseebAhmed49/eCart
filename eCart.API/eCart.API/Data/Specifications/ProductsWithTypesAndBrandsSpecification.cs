using System;
using eCart.API.Models;

namespace eCart.API.Data.Specifications
{
	public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
	{
		public ProductsWithTypesAndBrandsSpecification(ProductSpecParams specParams)
            : base(x =>
                (!specParams.brandId.HasValue || x.ProductBrandId == specParams.brandId) &&
                (!specParams.typeId.HasValue || x.ProductTypeId == specParams.typeId)
            )
		{
			AddInclude(x => x.ProductBrand);
			AddInclude(x => x.ProductType);
			AddOrderBy(x => x.Name);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1),
                specParams.PageSize);

			if(!string.IsNullOrEmpty(specParams.sort))
			{
                switch (specParams.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

		public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
		{
            AddInclude(x => x.ProductBrand);
            AddInclude(x => x.ProductType);
        }
    }
}


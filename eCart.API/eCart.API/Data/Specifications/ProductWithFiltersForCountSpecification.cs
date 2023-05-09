using System;
using eCart.API.Models;

namespace eCart.API.Data.Specifications
{
	public class ProductWithFiltersForCountSpecification: BaseSpecification<Product>
	{
		public ProductWithFiltersForCountSpecification(ProductSpecParams
			specParams) : base(x =>
                (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
                (!specParams.brandId.HasValue || x.ProductBrandId == specParams.brandId) &&
                (!specParams.typeId.HasValue || x.ProductTypeId == specParams.typeId)
            )
        {

		}
	}
}


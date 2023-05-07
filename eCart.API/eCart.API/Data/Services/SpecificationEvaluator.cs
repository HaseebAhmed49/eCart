using System;
using eCart.API.Data.Specifications;
using eCart.API.Models;
using Microsoft.EntityFrameworkCore;

namespace eCart.API.Services
{
	public class SpecificationEvaluator<TEntity> where TEntity: BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
			ISpecification<TEntity> spec)
		{
			var query = inputQuery;

			if(spec.Criteria != null)
			{
				query = query.Where(spec.Criteria); // p => p.ProductTypeId
			}

			query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

			return query;
		}
	}
}


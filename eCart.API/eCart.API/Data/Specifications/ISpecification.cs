﻿using System;
using System.Linq.Expressions;

namespace eCart.API.Data.Specifications
{
	public interface ISpecification<T>
	{
		Expression<Func<T, bool>> Criteria { get; }

		List<Expression<Func<T, object>>> Includes { get; }
	}
}


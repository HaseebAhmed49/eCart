using System;
using eCart.API.Data.Models;

namespace eCart.API.Data.Services.Basket
{
	public interface IBasketRepository
	{
		Task<CustomerBasket> GetBasketAsync(string basketId);

		Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);

		Task<bool> DeleteBasketAsync(string basketId);
	}
}


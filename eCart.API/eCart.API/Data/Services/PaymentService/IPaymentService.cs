using System;
using eCart.API.Data.Models;

namespace eCart.API.Data.Services.PaymentService
{
	public interface IPaymentService
	{
		Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
	}
}


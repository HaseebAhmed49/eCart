using System;
using eCart.API.Data.Models;
using eCart.API.Data.Models.OrderAggregate;

namespace eCart.API.Data.Services.PaymentService
{
	public interface IPaymentService
	{
		Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);

		Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);

        Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}


using System;
namespace eCart.API.Models
{
	public class Order
	{
		public int Id { get; set; }

		public int CustomerId { get; set; }

		public OrderStatus OrderStatus { get; set; }
	}

	public enum OrderStatus
	{
		Delievered,
		Closed,
		Hold,
		Shipped,
		New
	}
}


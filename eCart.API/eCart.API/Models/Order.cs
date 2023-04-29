using System;
namespace eCart.API.Models
{
	public class Order
	{
		public int Id { get; set; }

		public int CustomerId { get; set; }

		public OrderStatus OrderStatus { get; set; }

		public DateTime CreatedAt { get; set; }

		public DateTime UpdatedAt { get; set; }

		public int TotalAmount { get; set; }

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


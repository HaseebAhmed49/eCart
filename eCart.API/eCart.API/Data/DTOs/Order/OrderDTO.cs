using System;
using eCart.API.Data.DTOs.Identity;

namespace eCart.API.Data.DTOs.Order
{
	public class OrderDTO
	{
		public string BasketId { get; set; }

		public int DeliveryMethodId { get; set; }

		public AddressDTO ShipToAddress { get; set; }
	}
}


using System;
namespace eCart.API.Data.Models
{
	public class CustomerBasket
	{
		public CustomerBasket(string id)
		{
			Id = id;
		}

		public CustomerBasket()
		{
		}

        public string Id { get; set; }

        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}

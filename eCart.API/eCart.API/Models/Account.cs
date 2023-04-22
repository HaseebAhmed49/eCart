using System;
namespace eCart.API.Models
{
	public class Account
	{
		public int Id { get; set; }

		public string Address { get; set; }

		public bool isClosed { get; set; }

		public DateTime Open { get; set; }

		public DateTime Closed { get; set; }
	}
}
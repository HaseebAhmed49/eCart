using System;
namespace eCart.API.Models
{
	public class Customer
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string Address { get; set; }

		public string ContactNo { get; set; }

		public string EmailAddress { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.Now;

		public DateTime UpdatedAt { get; set; }
	}
}


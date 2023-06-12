using System;
using Microsoft.AspNetCore.Identity;

namespace eCart.API.Data.Models.Identity
{
	public class AppUser: IdentityUser
	{
		public string DisplayName { get; set; }

		public Address Address { get; set; }
	}
}


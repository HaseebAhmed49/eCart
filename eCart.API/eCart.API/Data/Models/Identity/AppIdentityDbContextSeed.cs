using System;
using Microsoft.AspNetCore.Identity;

namespace eCart.API.Data.Models.Identity
{
	public class AppIdentityDbContextSeed
	{
		public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
		{
			if(!userManager.Users.Any())
			{
				var user = new AppUser
				{
					DisplayName = "Haseeb",
					Email = "haseebahmed02@gmail.com",
					UserName = "haseebahmed02@gmail.com",
					Address = new Address
					{
						FirstName = "Haseeb",
						LastName = "Ahmed",
						Street = "10 The Street",
						City = "New York",
						State = "NY",
						ZipCode = "90210"
					}
				};

				await userManager.CreateAsync(user, "P@$$w0rd");
			}
		}
	}
}
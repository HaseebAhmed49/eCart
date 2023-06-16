using System;
using System.Security.Claims;
using eCart.API.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eCart.API.Data.Extensions
{
	public static class UserManagerExtensions
	{
		public static async Task<AppUser> FindUserByClaimsPrincipalWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
		{
			var email = user.FindFirstValue(ClaimTypes.Email);

			return await userManager.Users.Include(x => x.Address)
				.SingleOrDefaultAsync(x => x.Email == email);
		}

		public static async Task<AppUser> FindByEmailFromClaimPrincipal(this UserManager<AppUser> userManager, ClaimsPrincipal user)
		{
			return await userManager.Users.SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
		}
	}
}


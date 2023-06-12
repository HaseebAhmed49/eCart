using System;
using eCart.API.Data.Identity;
using eCart.API.Data.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eCart.API.Data.Extensions
{
	public static class IdentityServiceExtensions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services,
			IConfiguration config)
		{
			services.AddDbContext<AppIdentityDbContext>(opt =>
			{
				opt.UseSqlite(config.GetConnectionString("IdentityConnection"));
			});

			services.AddIdentityCore<AppUser>(opt =>
			{
				// Add Identity Options here
			}).AddEntityFrameworkStores<AppIdentityDbContext>()
			.AddSignInManager<SignInManager<AppUser>>();

			services.AddAuthentication();
			services.AddAuthorization();

			return services;
		}
	}
}


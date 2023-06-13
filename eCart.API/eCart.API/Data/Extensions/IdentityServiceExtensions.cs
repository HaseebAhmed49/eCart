using System;
using System.Text;
using eCart.API.Data.Identity;
using eCart.API.Data.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
						ValidIssuer = config["Token:Issuer"],
						ValidateIssuer = true,
					};
				});

			services.AddAuthorization();

			return services;
		}
	}
}


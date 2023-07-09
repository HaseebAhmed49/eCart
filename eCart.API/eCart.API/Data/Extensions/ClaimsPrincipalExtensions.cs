using System;
using System.Security.Claims;

namespace eCart.API.Data.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static string RetrieveEmailFromPrincipal(this ClaimsPrincipal user)
		{
            return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }
    }
}


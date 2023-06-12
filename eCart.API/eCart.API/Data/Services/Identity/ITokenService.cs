using System;
using eCart.API.Data.Models.Identity;

namespace eCart.API.Data.Services.Identity
{
	public interface ITokenService
	{
		string CreateToken(AppUser user);
	}
}


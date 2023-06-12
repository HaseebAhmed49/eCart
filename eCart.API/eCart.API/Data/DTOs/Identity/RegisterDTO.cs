using System;
namespace eCart.API.Data.DTOs.Identity
{
	public class RegisterDTO
	{
		public string DisplayName { get; set; }

		public string Email { get; set; }

		public string Password { get; set; }
	}
}
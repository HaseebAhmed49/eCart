using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCart.API.Data.DTOs.Identity;
using eCart.API.Data.Errors;
using eCart.API.Data.Extensions;
using eCart.API.Data.Models.Identity;
using eCart.API.Data.Services.Identity;
using eCart.API.Data.Services.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eCart.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMailService mailService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mailService = mailService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimPrincipal(User);

            return new UserDTO
            {
                Email = user.Email,
                // JSON Web Token
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        [HttpGet("email")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));

            if(user!=null && user.EmailConfirmed == true)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
                if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

                if (result.Succeeded)
                {
                    await _mailService.SendEmailAsyncBrevo(loginDTO.Email, user.DisplayName, "New Login", "<h1> Hey! new Login to your account notified</h1><p>New Login to your account at " + DateTime.Now + "</p>", "Login into E-Commerce Application");

                    // returning 200 Response Code with following data Username, email and JSON Token.
                    return new UserDTO
                    {
                        Email = user.Email,
                        // JSON Web Token
                        Token = _tokenService.CreateToken(user),
                        DisplayName = user.DisplayName
                    };
                }
            }
            // Using Microsoft Identity User Manager will check either the user exists with the passed email address.           
            else if (user != null && user.EmailConfirmed == false)
            {
                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
                string url = $"https://localhost:7167/api/Account/confirmemail?userid={user.Id}&token={validEmailToken}";

                await _mailService.SendEmailAsyncBrevo(user.Email, user.DisplayName, "Confirm your email", "<h1>Welcome to E-Commerce Application</h1>" +
                    $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>", "E-Commerce Application Registration Email");

                // Forbidden
                return StatusCode(403);
            }
            return Unauthorized(new ApiResponse(401));
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if(CheckEmailExistsAsync(registerDTO.Email).Result.Value)
            {
                return BadRequest(new ApiValidationErrorResponse{Errors = new[] {"Email Address is in use"}});
            }

            var user = new AppUser
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.Email
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
            string url = $"https://localhost:7167/api/Account/confirmemail?userid={user.Id}&token={validEmailToken}";

            await _mailService.SendEmailAsyncBrevo(user.Email, user.DisplayName, "Confirm your email", "<h1>Welcome to E-Commerce Application</h1>" +
                $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>", "E-Commerce Application Registration Email");

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return new UserDTO
            {
                Email = user.Email,
                // JSON Web Token
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        // /api/auth/confirmemail?userid&token
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmailAddress(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("User Not Found");
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);
            if (result.Succeeded)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return Ok(user);
            }
            return BadRequest(result);
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            var user = await _userManager.FindUserByClaimsPrincipalWithAddress(User);
            return _mapper.Map<Address, AddressDTO>(user.Address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO address)
        {
            var user = await _userManager.FindUserByClaimsPrincipalWithAddress(HttpContext.User);
            user.Address = _mapper.Map<AddressDTO, Address>(address);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDTO>(user.Address));
            return BadRequest("Problem updating the user");
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
        {
            List<UserDTO> users = new List<UserDTO>();
            var data = await _userManager.Users.ToListAsync();
            foreach (var user in _userManager.Users.ToList())
            {
                UserDTO temp = new UserDTO
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                };
                users.Add(temp);
            }
            return Ok(users);
        }
    }
}
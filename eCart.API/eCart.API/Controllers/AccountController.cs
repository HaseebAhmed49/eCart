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
        // Dependency Injection Services
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;

        // Constructor AccountController
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMailService mailService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mailService = mailService;
            _mapper = mapper;
        }

        // Get Current User EndPoint Only Accessible to Authorized Members
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimPrincipal(User);

            // return UserDTO
            return new UserDTO
            {
                Email = user.Email,
                // JSON Web Token
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        // CheckEmail of User EndPoint
        [HttpGet("email")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            // It will search the user by Email to verify either the user exists with the said email or not
            return await _userManager.FindByEmailAsync(email) != null;
        }

        // Login EndPoint
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            // Get User by Email
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            // Return Unauthorized If user is null
            if (user == null) return Unauthorized(new ApiResponse(401));

            // If user found
            if(user!=null && user.EmailConfirmed == true)
            {
                // Verifying Password
                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

                // If password doesn't matched
                if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

                // If user succeeded
                if (result.Succeeded)
                {
                    // Send Email that user login with date time
                    await _mailService.SendEmailAsyncBrevo(loginDTO.Email, user.DisplayName, "New Login", "<h1> Hey! new Login to your account notified</h1><p>New Login to your account at " + DateTime.Now + "</p>", "Login into E-Commerce Application");

                    //await _mailService.SendSMSAsyncBrevo("+923338437949", user.DisplayName, "Login into eCart Solution at " + DateTime.Now);

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
            // This part will be called, if the user is registered but email is not confirmed.
            else if (user != null && user.EmailConfirmed == false)
            {
                // It will generate confirmation email token
                var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // Encoded Email Token
                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                // Valid Email Token
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                // String for Email Confirmation
                string url = $"https://localhost:7167/api/Account/confirmemail?userid={user.Id}&token={validEmailToken}";

                // Send Email
                await _mailService.SendEmailAsyncBrevo(user.Email, user.DisplayName, "Confirm your email", "<h1>Welcome to E-Commerce Application</h1>" +
                    $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>", "E-Commerce Application Registration Email");

                // Forbidden
                return StatusCode(403);
            }
            return Unauthorized(new ApiResponse(401));
        }

        // Register User EndPoint
        // To Register New User
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            // Check if user exists with the following email
            if(CheckEmailExistsAsync(registerDTO.Email).Result.Value)
            {
                return BadRequest(new ApiValidationErrorResponse{Errors = new[] {"Email Address is in use"}});
            }

            // return user
            var user = new AppUser
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.Email
            };

            // Create User
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            // Generate Email Confirmation Token
            var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Encoded Email Token
            var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);

            // Valid Email Token
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);
            // Confirmation Email URL
            string url = $"https://localhost:7167/api/Account/confirmemail?userid={user.Id}&token={validEmailToken}";

            // Send Email
            await _mailService.SendEmailAsyncBrevo(user.Email, user.DisplayName, "Confirm your email", "<h1>Welcome to E-Commerce Application</h1>" +
                $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>", "E-Commerce Application Registration Email");

            // If result is not succeeded
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            return new UserDTO
            {
                Email = user.Email,
                // JSON Web Token
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        // Confirm Email Address EndPoint
        // /api/auth/confirmemail?userid&token
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmailAddress(string userId, string token)
        {
            // Check Null or White Spaces in userId or Token 
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            // Check user with userId
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("User Not Found");
            }

            // Decode Token
            var decodedToken = WebEncoders.Base64UrlDecode(token);

            // Normal Token
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            // Verify User
            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            // If User is genuine
            if (result.Succeeded)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
                return Ok(user);
            }
            return BadRequest(result);
        }

        // Authorized EndPoint Get User Address
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            // Check User Address
            var user = await _userManager.FindUserByClaimsPrincipalWithAddress(User);
            return _mapper.Map<Address, AddressDTO>(user.Address);
        }

        // Authorized Update User Adress EndPoint
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO address)
        {
            // Find User
            var user = await _userManager.FindUserByClaimsPrincipalWithAddress(HttpContext.User);

            // Update User Adress
            user.Address = _mapper.Map<AddressDTO, Address>(address);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDTO>(user.Address));
            return BadRequest("Problem updating the user");
        }

        // Authorized Get All Users EndPoint
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
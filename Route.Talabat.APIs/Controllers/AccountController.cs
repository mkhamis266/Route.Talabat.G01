using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.APIs.Errors;
using Route.Talabat.Core.Entities.Identity;
using Route.Talabat.Core.Services.Contract;

namespace Route.Talabat.APIs.Controllers
{

	public class AccountController : BaseApiController
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IAuthService _authService;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,IAuthService authService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_authService = authService;
		}

		[HttpPost("login")]
		public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is null) return Unauthorized(new ApiResponse(401, "Invalid Login"));

			var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
			if (!result.Succeeded) return Unauthorized(new ApiResponse(401, "Invalid Login"));

			return Ok(new UserDTO()
			{
				DisplayName = user.DisplayName,
				Email = model.Email,
				Token = await _authService.CreateTokenAsync(user, _userManager)
			}); ;
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDTO>> Register(RegisterDTO model)
		{
			// check if user already exsit
			var user = new ApplicationUser()
			{
				DisplayName = model.DisplayName,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
				UserName = model.Email.Split('@')[0],
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded) return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors.Select(E=>E.Description)});
			
			return Ok(new UserDTO() 
			{ 
				DisplayName = user.DisplayName, 
				Email = user.Email, 
				Token = await _authService.CreateTokenAsync(user, _userManager)
			});
		}

		[Authorize]
		[HttpGet]
		public async Task<ActionResult<UserDTO>> GetCurruntUSer()
		{
			var email = User.FindFirstValue(ClaimTypes.Email)?? String.Empty ;

			var user = await _userManager.FindByEmailAsync(email);

			return Ok(new UserDTO()
			{
				DisplayName = user.DisplayName,
				Email = email,
				Token = await _authService.CreateTokenAsync(user, _userManager)
			}) ;
		}
	}
}

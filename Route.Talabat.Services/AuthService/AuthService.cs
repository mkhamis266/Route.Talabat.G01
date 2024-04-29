using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Route.Talabat.Core.Entities.Identity;
using Route.Talabat.Core.Services.Contract;

namespace Route.Talabat.Services.AuthService
{
	public class AuthService : IAuthServices
	{
		private readonly IConfiguration _configuration;

		public AuthService(IConfiguration configuration)
        {
			_configuration = configuration;
		}
        public async Task<string> CreateTokenAsync(ApplicationUser user,UserManager<ApplicationUser> userManager)
		{
			var authCkaims = new List<Claim>() 
			{
				new Claim(ClaimTypes.Name, user.DisplayName),
				new Claim(ClaimTypes.Email, user.Email)
			};

			var userRoles = await userManager.GetRolesAsync(user);
			foreach (var role in userRoles)
			{
				authCkaims.Add(new Claim(ClaimTypes.Role, role));
			}

			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"] ?? string.Empty));

			var token = new JwtSecurityToken(
					audience: _configuration["JWT:ValidAudience"],
					issuer: _configuration["JWT:ValidIssuer"],
					expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:ValidIssuer"]??"0")),
					claims:authCkaims,
					signingCredentials: new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

	}
}

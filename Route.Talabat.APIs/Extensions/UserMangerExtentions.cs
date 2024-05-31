using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities.Identity;

namespace Route.Talabat.APIs.Extensions
{
	public static class UserMangerExtentions
	{
		public static async Task<ApplicationUser?> FindUserWihAddressAsync(this UserManager<ApplicationUser> userManager, ClaimsPrincipal user)
		{
			var email = user.FindFirstValue(ClaimTypes.Email);
			var userWithAddress = await userManager.Users.Include(U=>U.Address).FirstOrDefaultAsync(U=>U.NormalizedEmail == email.ToUpper());
			return userWithAddress;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Route.Talabat.Core.Entities.Identity;

namespace Route.Talabat.Infrastructure.Identity
{
	public static class ApplicationIdentityDbContextSeed
	{
		public static async Task DataSeedAsync(UserManager<ApplicationUser> userManger)
		{
			if (!userManger.Users.Any())
			{
				var user = new ApplicationUser()
				{
					DisplayName = "Mahmoud Khamis",
					Email = "m.khamis22@outlook.com",
					UserName = "Mahmoud.Khamis",
					PhoneNumber = "01003202138"
				};
				await userManger.CreateAsync(user,"P@ssw0rd");
			}
		}
	}
}

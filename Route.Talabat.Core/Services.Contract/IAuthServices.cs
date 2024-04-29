using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Route.Talabat.Core.Entities.Identity;

namespace Route.Talabat.Core.Services.Contract
{
	public interface IAuthServices
	{
		public Task<string> CreateTokenAsync(ApplicationUser User, UserManager<ApplicationUser> userManager);
	}
}

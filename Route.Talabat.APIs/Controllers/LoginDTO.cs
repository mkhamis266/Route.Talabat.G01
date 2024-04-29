using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.APIs.Controllers
{
	public class LoginDTO
	{
		[Required]
		public string Email { get; set; } = null!;

		[Required]
		public string Password { get; set; } = null!;

	}
}

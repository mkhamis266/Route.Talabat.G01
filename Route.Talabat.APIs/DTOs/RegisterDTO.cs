using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.APIs.DTOs
{
	public class RegisterDTO
	{
		[Required]
		public string DisplayName { get; set; } = null!;
		[Required]
		[EmailAddress]
		public string Email { get; set; } = null!;

		[Required]
		[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z]).{6,}$",ErrorMessage = "password must have 1 uppercase,nmust have 1 lowercase, must have 1 non-alphabetic and at least 6 characters")]
		public string Password { get; set; } = null!;

		[Required]
		public string PhoneNumber { get; set; } = null!;
	}
}

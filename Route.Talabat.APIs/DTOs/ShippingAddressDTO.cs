using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.APIs.DTOs
{
	public class ShippingAddressDTO
	{
		[Required]
		public string FirstName { get; set; } = null!;
		[Required]
		public string LastName { get; set; } = null!;
		[Required]
		public string Street { get; set; } = null!;
		[Required]
		public string City { get; set; } = null!;
		[Required]
		public string Country { get; set; } = null!;
	}
}

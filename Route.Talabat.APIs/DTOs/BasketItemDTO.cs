using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.APIs.DTOs
{
	public class BasketItemDTO
	{
		[Required]
		public int Id { get; set; }
		
		[Required]
		public string ProductName { get; set; }

		[Required]
		public string PictureUrl { get; set; }

		[Required]
		public string Category { get; set; }

		[Required]
		public string Brand { get; set; }

		[Required]
		[Range(0.1, double.MaxValue, ErrorMessage = "Price Should be more than 0")]
		public decimal Price { get; set; }

		[Required]
		[Range(1, int.MaxValue,ErrorMessage ="Quantity Should be at least 1")]
		public int Quantity { get; set; }
	}
}

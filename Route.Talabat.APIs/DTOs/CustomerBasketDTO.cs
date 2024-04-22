using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.APIs.DTOs
{
	public class CustomerBasketDTO
	{
		[Required]
		public string Id { get; set; }

		public List<BasketItemDTO> Items { get; set; }
	}
}

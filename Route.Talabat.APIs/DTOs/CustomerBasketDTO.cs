using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.APIs.DTOs
{
	public class CustomerBasketDTO
	{
		[Required]
		public string Id { get; set; }

		public List<BasketItemDTO> Items { get; set; }
		public string? PaymenyIntentId { get; set; }
		public string? ClientSecret { get; set; }
		public decimal ShippingPrice { get; set; }
		public int?  DeliveryMethodId { get; set; }
	}
}

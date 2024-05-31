using Route.Talabat.Core.Entities.Order_Aggregate;

namespace Route.Talabat.APIs.DTOs
{
	public class OrderItemDTO
	{
        public int Id { get; set; }

		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string PictureURL { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
	}
}
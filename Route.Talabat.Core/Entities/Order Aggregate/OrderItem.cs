using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Entities.Order_Aggregate
{
	public class OrderItem :BaseEntity
	{
        private OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrdered product, int quantity, decimal price)
		{
			Product = product;
			Quantity = quantity;
			Price = price;
		}

		public ProductItemOrdered Product { get; set; } = null!;
		public int Quantity { get; set; }
		public decimal Price { get; set; }
    }
}

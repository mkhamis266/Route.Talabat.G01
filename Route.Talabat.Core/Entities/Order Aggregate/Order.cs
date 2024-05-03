using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Entities.Order_Aggregate
{
	public class Order:BaseEntity
	{
        public string BuyerEmail { get; set; } = null!;

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; } = null!;

		DeliveyMethod DeliveyMethod { get; set; } = null!;

		ICollection<OrderItem> Items { get; set;} = new HashSet<OrderItem>();

        public decimal SubTotal { get; set; }

        public string PaymentIntentId { get; set; } = String.Empty;

        public decimal GetTotal() => SubTotal + DeliveyMethod.Cost;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities.Identity;

namespace Route.Talabat.Core.Entities.Order_Aggregate
{
	public class Order:BaseEntity
	{

        public Order()
        {
            
        }

        public Order(string buyerEmail,ShippingAddress shippingAddress,DeliveryMethod? deliveryMethod,ICollection<OrderItem> items,decimal subTotal,string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
			DelivreyMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }
        public string BuyerEmail { get; set; } = null!;

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public ShippingAddress ShippingAddress { get; set; } = null!;

		public DeliveryMethod? DelivreyMethod { get; set; } = null!;

		public ICollection<OrderItem> Items { get; set;} = new HashSet<OrderItem>();

        public decimal SubTotal { get; set; }

        public string PaymentIntentId { get; set; }

        public decimal GetTotal() => SubTotal + DelivreyMethod.Cost;
    }
}

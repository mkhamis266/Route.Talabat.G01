using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities.Order_Aggregate;

namespace Route.Talabat.Core.Entities.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }

        public string? PaymenyIntentId { get; set; }
        public string? ClintSecret { get; set; }

        public decimal? ShippingPrice { get; set; }
        public int? DeliveryMethodId { get; set; }

	}
}

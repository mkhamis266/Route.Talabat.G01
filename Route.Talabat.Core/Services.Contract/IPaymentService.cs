using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities.Basket;
using Route.Talabat.Core.Entities.Order_Aggregate;

namespace Route.Talabat.Core.Services.Contract
{
	public interface IPaymentService
	{
		Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);
		Task<Order?> UpdateOrderStatus(string paymentIntentId, bool isPaid);
	}
}

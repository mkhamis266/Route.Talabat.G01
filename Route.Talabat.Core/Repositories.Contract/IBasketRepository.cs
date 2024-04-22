using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities;

namespace Route.Talabat.Core.Repositories.Contract
{
	public interface IBasketRepository
	{
		Task<CustomerBasket?> GetCustomerBasket(string basketId);
		Task<CustomerBasket?> UpdateCustomerBasket(CustomerBasket basket);
		Task DeleteCustomerBasket(string basketId);
	}
}

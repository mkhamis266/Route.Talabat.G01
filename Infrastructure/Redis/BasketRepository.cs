using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Entities.Basket;
using Route.Talabat.Core.Repositories.Contract;
using StackExchange.Redis;

namespace Route.Talabat.Core.Redis
{
	public class BasketRepository : IBasketRepository
	{
		readonly private IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task DeleteCustomerBasket(string basketId)
		{
			await _database.KeyDeleteAsync(basketId);
		}

		public async Task<CustomerBasket?> GetCustomerBasket(string basketId)
		{
			var basket = await _database.StringGetAsync(basketId);
			return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
		}

		public async Task<CustomerBasket?> UpdateCustomerBasket(CustomerBasket basket)
		{
			var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize<CustomerBasket>(basket),TimeSpan.FromDays(7));
			if (IsCreatedOrUpdated is false) return null;
			return await GetCustomerBasket(basket.Id);
		}
	}
}

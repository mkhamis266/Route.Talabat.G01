using Route.Talabat.Core.Services.Contract;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Route.Talabat.Services.CavhingService
{
	internal class CachingService : ICachingService
	{
		private readonly IDatabase _database;
		public CachingService(IConnectionMultiplexer redis)
		{
			_database = redis.GetDatabase();
		}
		public async Task CacheResponseAsync(string key, object response, TimeSpan timeToLive)
		{
			if (response is null) return;

			//return json with camel case
			var jsonCamelCase = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

			//conver response to json
			var serializedResponse = JsonSerializer.Serialize(response, jsonCamelCase);

			//save response to redis dataBase
			await _database.StringSetAsync(key, serializedResponse, timeToLive);

		}

		public async Task<string?> GetCashedResponseAsync(string key)
		{
			//get the data using key
			var response = await _database.StringGetAsync(key);


			if (response.IsNullOrEmpty) return null;

			//return response
			return response;
		}
	}
}

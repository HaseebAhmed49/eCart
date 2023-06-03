using System;
using System.Text.Json;
using eCart.API.Data.Models;
using StackExchange.Redis;

namespace eCart.API.Data.Services.Basket
{
	public class BasketRepository: IBasketRepository
	{
        private readonly IDatabase _database;

		public BasketRepository(IConnectionMultiplexer redis)
		{
            _database = redis.GetDatabase();
		}

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.Id,
                JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

            // Updating the existing basket for the user rather than creating new everytime
            // Not going to Update individual values
            // Depends on Business Logic
            // How much memory, How many customers, how much memory on server

            if (!created) return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}


using System;
using System.Text.Json;
using System.Threading.Tasks;
using AmazingShop.Core.Entities;
using AmazingShop.Core.Interfaces;
using StackExchange.Redis;

namespace AmazingShop.Infrastructure
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await database.StringGetAsync(basketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await database.StringSetAsync(basket.Id, 
                JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
 
            if (!created) return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}
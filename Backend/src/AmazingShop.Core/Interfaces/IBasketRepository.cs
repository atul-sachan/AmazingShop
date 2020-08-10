using System.Threading.Tasks;
using AmazingShop.Core.Entities;

namespace AmazingShop.Core.Interfaces
{
    public interface IBasketRepository
    {
         Task<CustomerBasket> GetBasketAsync(string basketId);
         Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
         Task<bool> DeleteBasketAsync(string basketId);
    }
}
using System.Threading.Tasks;
using AmazingShop.Core.Entities;
using AmazingShop.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmazingShop.Api.Controllers
{
    public class BasketController: BaseApiController
    {
        private readonly IBasketRepository basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id){
            var basket = await basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket());
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket){
            var updatedBasket = await basketRepository.UpdateBasketAsync(basket);
            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id){
            await basketRepository.DeleteBasketAsync(id);
        }
    }
}
using Grpc.Core;
using Neova.Basket.API.Protos;

namespace Neova.Basket.API.Services
{
    public class CustomBasketService : BasketService.BasketServiceBase
    {
        public override Task<BasketResponse> GetBasket(GetBasketRequest request, ServerCallContext context)
        {
            var response = new BasketResponse
            {
                UserId = request.UserId,

                Items = { new BasketItem { ProductId = "123", Quantity = 2, Price = 10.0 } }
            };

            return Task.FromResult(response);
        }

        public override Task<BasketResponse> AddItem(AddItemRequest request, ServerCallContext context)
        {
            var response = new BasketResponse
            {
                UserId = request.UserId,
                Items = { new BasketItem { ProductId = request.Item.ProductId, Quantity = request.Item.Quantity, Price = request.Item.Price } }
            };
            return Task.FromResult(response);
        }

        public override Task<BasketResponse> Update(UpdateBasketRequest request, ServerCallContext context)
        {
            var response = new BasketResponse
            {
                UserId = request.UserId,
                Items = { new BasketItem { ProductId = request.Items[0].ProductId, Quantity = request.Items[0].Quantity, Price = request.Items[0].Price } }
            };
            return Task.FromResult(response);
        }

    }
}

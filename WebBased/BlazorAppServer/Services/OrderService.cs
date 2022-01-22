using BlazorAppServer.Base;
using EBazarModels.Models;

namespace BlazorAppServer.Services
{
    public class OrderService : IOrderService
    {
        public Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            throw new NotImplementedException();
        }

        public Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            throw new NotImplementedException();
        }
    }
}

using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAppServer.Pages.Orders
{
    public partial class OrderHome:ComponentBase
    {
        [Inject]
        public IOrdersService _ordersService { get; set; }

        public List<Order> OrderList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            string userId = "";// User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = "";// User.FindFirstValue(ClaimTypes.Role);

            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
        }
    }
}

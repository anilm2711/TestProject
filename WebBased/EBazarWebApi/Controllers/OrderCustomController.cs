using EBazarWebApi.Data;
using EBazarWebApi.Data.Cart;
using EBazarWebApi.Data.Services;
using EBazarWebApi.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EBazarWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderCustomController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        private readonly AppDbContext _context;
        public OrderCustomController(AppDbContext context, ShoppingCart shoppingCart)
        {
            _context = context;
            _shoppingCart = shoppingCart;
        }

        [HttpGet]
        public  string ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            ShoppingCartVM response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            string json = JsonConvert.SerializeObject(response, Formatting.Indented, new JsonSerializerSettings
            { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            
            return json;
            //return response;
        }

    }
}

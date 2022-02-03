using BlazorAppServer.Services;
using EBazarAppServer.Data.Cart;
using EBazarAppServer.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorAppServer.Pages.Orders
{
    public partial class ShoppingCartView : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }
        public ShoppingCartVM shoppingCartVM { get; set; }

        [Inject]
        private  IMoviesService _moviesService { get; set; }
        [Inject]
        private  ShoppingCart _shoppingCart { get; set; }
        [Inject]
        private  IOrdersService _ordersService { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                var item = await _moviesService.GetMovieByIdAsync(Convert.ToInt32(Id));
                if (item != null)
                {
                    _shoppingCart.AddItemToCart(item);
                }
                ShoppingCart(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveItemFromShoppingCart(MouseEventArgs e)
        {
            var item = await _moviesService.GetMovieByIdAsync(Convert.ToInt32(Id));

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
        }
        public async Task AddItemToShoppingCart(MouseEventArgs e)
        {
            var item = await _moviesService.GetMovieByIdAsync(Convert.ToInt32(Id));
            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
        }

        public ShoppingCartVM ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            shoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return shoppingCartVM;
        }

    }
}

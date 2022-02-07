using BlazorAppServer.Services;
using BlazorAppServer.SessionStorage;
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
        [Inject]
        ISessionStorageService sessionStorage { get; set; }

        [Parameter]
        public string Id { get; set; }
        public ShoppingCartVM shoppingCartVM { get; set; } = new ShoppingCartVM();

        [Inject]
        private  IMoviesService _moviesService { get; set; }

        [Inject]
        private  ShoppingCart _shoppingCart { get; set; }

        [Inject]
        private  IOrdersService _ordersService { get; set; }

        private string CartId { get; set; }

        private string sessionValue { get; set; }

        public bool? FstRender { get; set; } = null;

        public string NameFromSessionStorage { get; set; }
        public int ItemInSessionStorage { get; set; }
        public string Name { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            FstRender = firstRender;
            if (firstRender)
            {
                await GetNameFromSessionStorage();
                await GetSessionStorageLenght();
                if(string.IsNullOrEmpty(NameFromSessionStorage))
                {
                    Name = Guid.NewGuid().ToString();
                    await SaveName();
                }
                _shoppingCart.ShoppingCartId = NameFromSessionStorage;
                await AddItemToCart();
            }
           

        }

        protected override async Task OnInitializedAsync()
        {
            if (FstRender == false)
            {
              await  AddItemToCart();
            }
        }
        protected async Task AddItemToCart()
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
            catch (Exception )
            {
                throw;
            }
        }

        public async Task RemoveItemFromShoppingCart(int movieId)
        {
            var item = await _moviesService.GetMovieByIdAsync(movieId);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }

        }
        public async Task AddItemToShoppingCart(int movieId)
        {
            var item = await _moviesService.GetMovieByIdAsync(movieId);
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
            StateHasChanged();
            return shoppingCartVM;
        }

        async Task SaveName()
        {
            await sessionStorage.SetItemAsync("name", Name);
            await GetNameFromSessionStorage();
            await GetSessionStorageLenght();
        }

        async Task RemoveName()
        {
            await sessionStorage.RemoveItemAsync("name");
            await GetNameFromSessionStorage();
            await GetSessionStorageLenght();
        }
        async Task ClearSessionStorage()
        {
            await sessionStorage.ClearAsync();
            await GetNameFromSessionStorage();
            await GetSessionStorageLenght();
        }

        async Task GetNameFromSessionStorage()
        {
            NameFromSessionStorage = await sessionStorage.GetItemAsync<string>("name");
            if (string.IsNullOrEmpty(NameFromSessionStorage))
            {
                NameFromSessionStorage = "";
            }
        }

        async Task GetSessionStorageLenght()
        {
            ItemInSessionStorage = await sessionStorage.LengthAsync();
        }

    }
}

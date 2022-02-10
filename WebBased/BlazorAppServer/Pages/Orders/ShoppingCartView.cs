using BlazorAppServer.Data.ViewComponents;
using BlazorAppServer.Services;
using EBazarAppServer.Data.Cart;
using EBazarAppServer.ViewModels;
using Microsoft.AspNetCore.Components;

namespace BlazorAppServer.Pages.Orders
{
    public partial class ShoppingCartView : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }
        public ShoppingCartVM shoppingCartVM { get; set; } = new ShoppingCartVM();

        [Inject]
        private  IMoviesService _moviesService { get; set; }

        [Inject]
        private IOrdersService _ordersService { get; set; }

        [Inject]
        private  ShoppingCart _shoppingCart { get; set; }

        [Parameter]
        public EventCallback<int> OnMovieSelection { get; set; }

        [Inject]
        public ItemsInCart currentPage { get; set; }

        private string count { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await AddItemToCart();
        }

        private void CountItems(int countIt)
        {
            if (countIt == 0)
                count = String.Empty;
            else
                count = Convert.ToString(countIt);

            currentPage.SetCurrentPageName(count);
            ChangeName();
        }
        public void ChangeName()
        {
            currentPage.SetCurrentPageName(count);
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
          await  ShoppingCart();
            
        }
        public async Task AddItemToShoppingCart(int movieId)
        {
            var item = await _moviesService.GetMovieByIdAsync(movieId);
            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            await OnMovieSelection.InvokeAsync(4);
            await ShoppingCart();
        }
        public async Task<ShoppingCartVM> ShoppingCart()
        {
            var items =_shoppingCart.GetShoppingCartItems();

            _shoppingCart.ShoppingCartItems = items;

            shoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            CountItems(items.Count);
            return shoppingCartVM;
        }

        public async Task CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = "anil";//   User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = "fakeemail.@fake.com";//User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            navigationManager.NavigateTo("OrderCompleted");
        }



    }
}

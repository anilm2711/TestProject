using EBazarAppServer.Data.Cart;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorAppServer.SessionStorage
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazoredSessionStorage(this IServiceCollection services)
        {
            return services.AddScoped<ISessionStorageService, SessionStorageService>()
                .AddScoped(sc => 
                {
                   Task<ShoppingCart> spc = new ShoppingCart().GetShoppingCart(sc);
                    return spc;
                });
        }
    }
}

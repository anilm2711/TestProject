using EBazarAppServer.ViewModels;
using EBazarModels.Data.ViewModels;

namespace BlazorAppServer.Services
{
    public interface ILoginService
    {
       Task<HttpResponseMessage> Login(LoginVM loginVM);

        Task<HttpResponseMessage> Register(RegisterVM registerVM);
    }
}

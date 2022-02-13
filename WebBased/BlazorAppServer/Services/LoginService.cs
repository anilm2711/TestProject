using EBazarAppServer.ViewModels;
using EBazarModels.Data.ViewModels;

namespace BlazorAppServer.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient  _httpClient;

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> Login(LoginVM loginVM)
        {
           return await _httpClient.PostAsJsonAsync("api/Account/Login", loginVM);
        }

        public async Task<HttpResponseMessage> Register(RegisterVM registerVM)
        {
            return await _httpClient.PostAsJsonAsync("api/Account/Register", registerVM);
        }
    }
}

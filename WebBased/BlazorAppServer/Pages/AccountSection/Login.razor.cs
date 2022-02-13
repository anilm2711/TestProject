using BlazorAppServer.Services;
using EBazarAppServer.Data;
using EBazarAppServer.ViewModels;
using EBazarModels.Data.ViewModels;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorAppServer.Pages.AccountSection
{
    public partial class Login : ComponentBase
    {

        [Inject]
        public ILoginService service { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        public LoginVM loginVM { get; set; }

         public string Error { get; set; }

        protected override void OnInitialized()
        {
            loginVM = new LoginVM() { EmailAddress = "admin@etickets.com", Password = "Coding@1234?", ErrorMessage = "", IsSucceed = false };
        }
        public async Task HandleOnValidSubmit()
        {
            HttpResponseMessage httpResponseMessage =await service.Login(loginVM);
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            loginVM = JsonConvert.DeserializeObject<LoginVM>(responseString);

            if (loginVM != null && loginVM.IsSucceed == true)
            {
                navigationManager.NavigateTo("/");
            }
            else
            {
                Error = loginVM.ErrorMessage;
                navigationManager.NavigateTo("login");
            }
        }
    }
}

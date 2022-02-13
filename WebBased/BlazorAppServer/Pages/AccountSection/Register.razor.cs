using BlazorAppServer.Services;
using EBazarAppServer.ViewModels;
using EBazarModels.Data.ViewModels;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorAppServer.Pages.AccountSection
{
    public partial class Register : ComponentBase
    {
        [Inject]
        public ILoginService _service { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        public RegisterVM RegisterVM { get; set; } = new RegisterVM();

        protected override void OnInitialized()
        {
            RegisterVM = new RegisterVM() { FullName = "", EmailAddress = "", Password = "", ConfirmPassword = "", ErrorMessage = "" };
        }

        public async Task HandleOnValidSubmit()
        {
            HttpResponseMessage httpResponseMessage = await _service.Register(RegisterVM);
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            RegisterVM = JsonConvert.DeserializeObject<RegisterVM>(responseString);

            if (RegisterVM != null && string.IsNullOrEmpty(RegisterVM.ErrorMessage) == true)
            {
                navigationManager.NavigateTo("/");
            }
            else 
            {
                navigationManager.NavigateTo("Register");
            }
        }
    }
}

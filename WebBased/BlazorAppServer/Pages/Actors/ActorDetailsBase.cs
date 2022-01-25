using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAppServer.Pages
{
    public class ActorDetailsBase:ComponentBase
    {
        [Parameter]
        public string Id { get; set; }
        [Inject]
        public IActorService service { get; set; }
        public Actor result { get; set; } 

        protected string ButtonText { get; set; } = "Hide Footer";
        protected string? CssClass { get; set; } = null;
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Id = Id ?? "1";
            result = (await service.GetResultByIdAsync($"api/Actors/{Id}"));
        }

        protected void Button_Click()
        {
            if(ButtonText == "Hide Footer")
            {
                ButtonText = "Show Footer";
                CssClass = "hide-footer";
            }
            else
            {
                ButtonText = "Hide Footer";
                CssClass = null;
            }
        }
        protected async Task DeleteActor()
        {
            //string Id = Convert.ToString(Id);
            var result = service.DeleteAsync($"api/Actors/{Id}");
            if (result != null)
            {
                NavigationManager.NavigateTo("/actor", true);
            }
        }
    }

}

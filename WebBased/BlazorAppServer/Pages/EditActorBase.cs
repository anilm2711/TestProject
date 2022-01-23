using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAppServer.Pages
{
    public class EditActorBase:ComponentBase
    {
        [Inject]
        public IActorService service { get; set; }
        public Actor Actor { get; set; } = new Actor();

        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public string picUrl { get; set; } = string.Empty;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Id = Id ?? "1";
            Actor =await service.GetResultByIdAsync($"api/Actors/{Id}");
            picUrl = Actor.ProfilePictureURL;
        }

        protected async Task HandleValidSubmit()
        {
            var result = await service.UpdateAsync($"api/Actors/{Id}",Actor);
            if (result != null)
            {
                NavigationManager.NavigateTo("/actor");
            }
        }

    }
}

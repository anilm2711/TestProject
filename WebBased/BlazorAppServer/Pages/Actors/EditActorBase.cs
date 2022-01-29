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
            int.TryParse(Id, out int actorId);
            if (actorId != 0)
            {
                Actor = await service.GetResultByIdAsync($"api/Actors/{Id}");
                picUrl = Actor.ProfilePictureURL;
            }
            else
            {
                Actor = new Actor();
                Actor.ProfilePictureURL = picUrl;
            }
            
        }

        protected async Task HandleValidSubmit()
        {
            Actor.ProfilePictureURL = picUrl;
            HttpResponseMessage result;
            if (Actor.Id > 0)
            {
                result = await service.UpdateAsync($"api/Actors/{Id}", Actor);
            }
            else
            {
                result = await service.AddAsync("api/Actors", Actor);
            }
            if (result != null)
            {
                NavigationManager.NavigateTo("/actor", true);
            }
        }

    }
}

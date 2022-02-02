using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAppServer.Pages
{
    public class EditActorBase:ComponentBase
    {

           [Inject]
        public IActorsService service { get; set; }
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
                Actor = await service.GetByIdAsync(actorId);
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

            if (Actor.Id > 0)
            {
                service.UpdateAsync(Convert.ToInt32(Id), Actor);
            }
            else
            {
                service.AddAsync(Actor);
            }
            NavigationManager.NavigateTo("/actor", true);
        }

    }
}

using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAppServer.Pages
{
    public class ActorListBase : ComponentBase
    {
        [Inject]
        public IActorService service { get; set; }
        public IEnumerable<Actor> Actors { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Actors = (await service.GetResult("api/Actors")).ToList();
        }
    }
}

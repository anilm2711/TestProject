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

        public bool ShowFooter { get; set; } = true;
        protected override async Task OnInitializedAsync()
        {
            Actors = (await service.GetResult("api/Actors")).ToList();
        }
        protected int SelectedActorCount { get; set; }=0;
        protected void ActorSelectionChanged(bool IsSelected)
        {
            if(IsSelected)
            {
                SelectedActorCount++;
            }
            else
            {
                SelectedActorCount--;
            }

        }
    }
}

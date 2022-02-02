using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BlazorAppServer.Pages
{
    public class ActorListBase : ComponentBase
    {
        [Inject]
        public IActorsService service { get; set; }
        public IEnumerable<Actor> Actors { get; set; }

        public bool ShowFooter { get; set; } = true;
        protected override async Task OnInitializedAsync()
        {
            //Actors = (await service.GetResult("api/Actors")).ToList();
            Actors = await service.GetAllAsync();

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

        protected async Task ActorDeleted()
        {
            Actors = await service.GetAllAsync();
        }
    }
}

using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

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
            //Actors = (await service.GetResult("api/Actors")).ToList();
            Task<string> json = service.GetResultSerialize("api/Actors");
            Actors = JsonConvert.DeserializeObject<List<Actor>>(json.Result, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
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

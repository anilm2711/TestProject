using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BlazorAppServer.Pages.Movies
{
    public class MovieCreateBase : ComponentBase
    {
        [Inject]
        public IActorService service { get; set; }

        [Inject]
        public IProducerService serviceProducer { get; set; }

        [Inject]
        public ICinemaService serviceCinema { get; set; }
        public IEnumerable<Cinema> Cinemas { get; set; }
        public NewMovieVM newMovieVM { get; set; } = new NewMovieVM();

        public IEnumerable<Actor> Actors { get; set; }
        public IEnumerable<Producer> Producers { get; set; }


        [Inject]
        public NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Task<string> json = service.GetResultSerialize("api/Actors");
            Actors = JsonConvert.DeserializeObject<List<Actor>>(json.Result, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            Producers = (await serviceProducer.GetResult("api/Producers"));
            Cinemas = (await serviceCinema.GetResult("api/Cinemas"));
            NewMovieDropdownsVM newMovieDropdownsVM = new NewMovieDropdownsVM
            {
                Actors = Actors,
                Producers = Producers,
                Cinemas = Cinemas
            };
        }
        protected async Task HandleValidSubmit()
        {
            navigationManager.NavigateTo("/", true);
        }

        protected void  OnActorSelectDropDownChange(ChangeEventArgs e)
        {
            string g = Convert.ToString( e.Value);
            
        }
    }
}

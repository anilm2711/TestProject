using BlazorAppServer.Data;
using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BlazorAppServer.Pages.Movies
{
    public class MovieCreateBase : ComponentBase
    {
        [Inject]
        public IMovieService serviceMV { get; set; }

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

        [Parameter]
        public NewMovieVM MV { get; set; } = new NewMovieVM();
        [Inject]
        public NavigationManager navigationManager { get; set; }

        IEnumerable<string> ActorIds { get; set; }

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

            newMovieVM = new NewMovieVM()
            {
                Name ="kdsfsdf",
                Description = "kdsfsdf",
                Price = 10.00,
                ImageURL = "kdsfsdf",
                CinemaId =1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                MovieCategory = EBazarModels.Data.Enum.MovieCategory.Horror,
                ProducerId = 2
            };

        }

        protected async Task HandleValidSubmit()
        {
            //foreach (string pid in ActorIds)
            //{
            //    bool isNumber = int.TryParse(pid, out int actorid);
            //    if (isNumber)
            //    {
            //        newMovieVM.ActorIds.Add(actorid);
            //    }
            //}
            HttpResponseMessage result;

            //result = await serviceMV.AddAsync("api/Movies", MV);

            //if (result != null)
            //{
                navigationManager.NavigateTo("/actor", true);
            //}
        }


        protected void  OnActorSelectDropDownChange(ChangeEventArgs e)
        {
            ActorIds = (IEnumerable<string>)e.Value;
            
        }
    }
}

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
        public IMovieCustomService serviceMV { get; set; }

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
            //MV = new NewMovieVM()
            //{
            //    Id=-1,
            //    Name = "NWWN",
            //    Description = "MSMMSMS",
            //    Price = 10.00,
            //    ImageURL = "http://dotnethow.net/images/movies/movie-3.jpeg",
            //    CinemaId = 1,
            //    StartDate = DateTime.Now,
            //    EndDate = DateTime.Now,
            //    MovieCategory = EBazarModels.Data.Enum.MovieCategory.Horror,
            //    ProducerId = 2
            //};
            HttpResponseMessage result;

            result = await serviceMV.AddMovie("api/MoviesCustom", MV);

            if (result != null)
            {
                navigationManager.NavigateTo("/", true);
            }
        }


        protected void  OnActorSelectDropDownChange(ChangeEventArgs e)
        {
            ActorIds = (IEnumerable<string>)e.Value;
            
        }
    }
}

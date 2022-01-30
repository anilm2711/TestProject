using BlazorAppServer.Data;
using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BlazorAppServer.Pages.Movies
{
    public class MovieCreateBase : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }
        [Inject]
        public IMovieCustomService serviceCustomMV { get; set; }

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

        public Movie data { get; set; }

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
            bool x= int.TryParse(Id, out int mvId);
            if (mvId > 0)
            {
                try
                {
                    Id = Id ?? "1";
                    Task<string> movieSrz = service.GetResultSerialize($"api/Movies/{Id}");
                    data = JsonConvert.DeserializeObject<Movie>(movieSrz.Result, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

                    MV.Id = mvId;
                    MV.Price = data.Price;
                    MV.ImageURL = data.ImageURL;
                    MV.Name = data.Name;
                    MV.CinemaId = data.CinemaId;
                    MV.StartDate = data.StartDate;
                    MV.EndDate = data.EndDate;
                    MV.MovieCategory = data.MovieCategory;
                    MV.ProducerId = data.ProducerId;
                    MV.Description = data.Description;
                    MV.ActorIds = data.Actors_Movies.Select(p => p.ActorId).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected async Task HandleValidSubmit()
        {
            foreach (string pid in ActorIds)
            {
                bool isNumber = int.TryParse(pid, out int actorid);
                if (isNumber)
                {
                    MV.ActorIds.Add(actorid);
                }
            }

            HttpResponseMessage result;

            result = await serviceCustomMV.AddMovie("api/MoviesCustom", MV);

            if (result != null)
            {
                navigationManager.NavigateTo("/", true);
            }
        }

        protected void ActorSelect_OnClicK(ChangeEventArgs e)
        {
            ActorIds = (IEnumerable<string>)e.Value;
        }
    }
}

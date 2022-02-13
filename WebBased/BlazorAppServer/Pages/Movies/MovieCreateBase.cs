
using BlazorAppServer.Services;
using EBazarAppServer.ViewModels;
using EBazarModels.Data.ViewModels;
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
        public IMoviesService serviceMV { get; set; }

        [Inject]
        public IActorsService service { get; set; }

        [Inject]
        public IProducersService serviceProducer { get; set; }

        [Inject]
        public ICinemasService serviceCinema { get; set; }
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
            Actors = await service.GetAllAsync();
            Producers = await serviceProducer.GetAllAsync();
            Cinemas = await serviceCinema.GetAllAsync();
            NewMovieDropdownsVM newMovieDropdownsVM = new NewMovieDropdownsVM
            {
                Actors = Actors.ToList(),
                Producers = Producers.ToList(),
                Cinemas = Cinemas.ToList()
            };
            bool x= int.TryParse(Id, out int mvId);
            if (mvId > 0)
            {
                try
                {
                    Id = Id ?? "1";
                    data= await serviceMV.GetMovieByIdAsync(mvId);
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
            else
            {
                MV = new NewMovieVM();
            }

        }

        protected async Task HandleValidSubmit()
        {

            try
            {
                if (MV.Id <= 0)
                {
                    MV.ActorIds = new List<int>();
                    if (ActorIds != null)
                    {
                        foreach (string pid in ActorIds)
                        {
                            bool isNumber = int.TryParse(pid, out int actorid);
                            if (isNumber)
                            {
                                MV.ActorIds.Add(actorid);
                            }
                        }
                    }
                    serviceMV.AddNewMovieAsync(MV);
                }
                else
                {

                    if (ActorIds != null)
                    {
                        MV.ActorIds = new List<int>();
                        foreach (string pid in ActorIds)
                        {
                            bool isNumber = int.TryParse(pid, out int actorid);
                            if (isNumber)
                            {
                                MV.ActorIds.Add(actorid);
                            }
                        }
                    }
                    serviceMV.UpdateMovieAsync(MV);
                }

            }
            catch (Exception)
            {

                throw;
            }

            navigationManager.NavigateTo("/", true);

        }

        protected void ActorSelect_OnClicK(ChangeEventArgs e)
        {
            ActorIds = (IEnumerable<string>)e.Value;
        }
    }
}

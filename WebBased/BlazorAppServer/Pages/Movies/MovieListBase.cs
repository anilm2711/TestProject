using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BlazorAppServer.Pages
{
    public class MovieListBase : ComponentBase
    {
        [Inject]
        public IMovieService service { get; set; }
        public IEnumerable<Movie> resultList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Task<string> json =  service.GetResultSerialize("api/Movies");
            resultList = JsonConvert.DeserializeObject<List<Movie>>(json.Result, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
        }
    }
}

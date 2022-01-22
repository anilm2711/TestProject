using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAppServer.Pages
{
    public class MovieListBase : ComponentBase
    {
        [Inject]
        public IMovieService service { get; set; }
        public IEnumerable<Movie> resultList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            resultList = (await service.GetResult("api/Movies")).ToList();
        }
    }
}

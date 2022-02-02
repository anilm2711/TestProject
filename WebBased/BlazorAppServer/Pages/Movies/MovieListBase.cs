using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BlazorAppServer.Pages
{
    public class MovieListBase : ComponentBase
    {
        [Inject]
        public IMoviesService service { get; set; }
        public IEnumerable<Movie> resultList { get; set; }

        [Parameter]
        public string value { get; set; }

        protected override async Task OnInitializedAsync()
        {
            string s = value;
            resultList = await service.GetAllAsync(x=>x.Cinema);

        }
    }
}

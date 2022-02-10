using BlazorAppServer.Data.ViewComponents;
using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace BlazorAppServer.Pages
{
    public class MovieListBase : ComponentBase
    {
        
        [Inject]
        public SearchItems searchItem { get; set; }

        [Inject]
        public IMoviesService service { get; set; }
        public IEnumerable<Movie> resultList { get; set; }

        [Parameter]
        public string value { get; set; }

        protected override async Task OnInitializedAsync()
        {
            resultList = await service.GetAllAsync(x => x.Cinema);
            searchItem.OnChange += StateHasChanged;
            if (string.IsNullOrEmpty(value) == false)
            {
                resultList = resultList.Where(p => p.Name.ToLower().Contains(value.ToLower())
                || p.Description.ToLower().Contains(value.ToLower())).ToList();
            }
        }
    }
}

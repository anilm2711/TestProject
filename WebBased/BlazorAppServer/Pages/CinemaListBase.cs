using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAppServer.Pages
{
    public class CinemaListBase : ComponentBase
    {
        [Inject]
        public ICinemaService service { get; set; }
        public IEnumerable<Cinema> Cinemas { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Cinemas = (await service.GetResult("api/Cinemas")).ToList();
        }
    }
}

using BlazorAppServer.Services;
using EBazarModels.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAppServer.Pages
{
    public class ProducerListBase : ComponentBase
    {
        [Inject]
        public IProducersService producerService { get; set; }
        public IEnumerable<Producer> Producers { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            Producers = (await producerService.GetAllAsync()).ToList();
        }
    }
}

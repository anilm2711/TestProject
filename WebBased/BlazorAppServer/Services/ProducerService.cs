using BlazorAppServer.Base;
using EBazarModels.Models;

namespace BlazorAppServer.Services
{
    public class ProducerService : EntityBaseRepository<Producer>, IProducerService
    {
        public ProducerService(HttpClient httpClient) : base(httpClient)
        {

        }

    }
}
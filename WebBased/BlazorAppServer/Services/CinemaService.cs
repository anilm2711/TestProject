using BlazorAppServer.Base;
using EBazarModels.Models;

namespace BlazorAppServer.Services
{
    public class CinemaService: EntityBaseRepository<Cinema>,ICinemaService
    {
        public CinemaService(HttpClient httpClient) : base(httpClient)
        {

        }
    }
}

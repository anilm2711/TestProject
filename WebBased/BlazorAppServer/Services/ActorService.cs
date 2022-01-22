using BlazorAppServer.Base;
using EBazarModels.Models;
using System.Linq.Expressions;

namespace BlazorAppServer.Services
{
    public class ActorService :  EntityBaseRepository<Actor>, IActorService
    {
        private readonly HttpClient _httpClient;
        public ActorService(HttpClient httpClient):base(httpClient)
        {
  
        }

    }
}

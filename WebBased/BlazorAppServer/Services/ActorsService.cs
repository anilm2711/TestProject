using BlazorAppServer.Base;
using EBazarAppServer.Data;
using EBazarModels.Models;
using System.Linq.Expressions;

namespace BlazorAppServer.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        private readonly HttpClient _httpClient;
        public ActorsService(AppDbContext context) : base(context)
        {
        }
    }
}

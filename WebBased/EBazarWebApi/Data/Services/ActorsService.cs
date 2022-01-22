using EBazarModels.Models;
using EBazarWebApi.Data.Base;

namespace EBazarWebApi.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        private readonly HttpClient _httpClient;
        public ActorsService(AppDbContext context) : base(context)
        {
        }
    }
}

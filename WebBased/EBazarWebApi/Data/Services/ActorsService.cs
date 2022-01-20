using EBazarModels.Models;
using EBazarWebApi.Data.Base;

namespace EBazarWebApi.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDbContext context) : base(context) { }
    }
}

using EBazarModels.Models;
using EBazarWebApi.Data;
using EBazarWebApi.Data.Base;

namespace EBazarWebApi.Data.Services
{
    public class ProducersService: EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext context) : base(context)
        {
        }
    }
}

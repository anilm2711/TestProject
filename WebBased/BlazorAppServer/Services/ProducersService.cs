using BlazorAppServer.Base;
using EBazarAppServer.Data;
using EBazarModels.Models;

namespace BlazorAppServer.Services
{
    public class ProducersService : EntityBaseRepository<Producer>, IProducersService
    {
        public ProducersService(AppDbContext context) : base(context)
        {
        }
    }
}
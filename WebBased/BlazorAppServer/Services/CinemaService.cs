using BlazorAppServer.Base;
using EBazarAppServer.Data;
using EBazarModels.Models;

namespace BlazorAppServer.Services
{
    public class CinemasService : EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext context) : base(context)
        {
        }
    }
}

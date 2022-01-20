using EBazarModels.Models;
using EBazarWebApi.Data.Base;

namespace EBazarWebApi.Data.Services
{
    public class CinemasService:EntityBaseRepository<Cinema>, ICinemasService
    {
        public CinemasService(AppDbContext context) : base(context)
        {
        }
    }
}

using EBazarModels.Models;
using System.Linq.Expressions;

namespace BlazorAppServer.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class,IEntityBase, new()
    {
        private readonly HttpClient _context;
        public EntityBaseRepository(HttpClient context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetResult(string uri)
        {
            Task<T[]?> c = _context.GetFromJsonAsync<T[]>(uri);
            return await c;
        }

        public async Task<T> GetResultByIdAsync(string uri)
        {
            Task<T?> c = _context.GetFromJsonAsync<T>(uri);
            return await c;
        }

    }
}

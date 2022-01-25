using EBazarModels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Text.Json;

namespace BlazorAppServer.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class,IEntityBase, new()
    {
        private readonly HttpClient _context;
        public EntityBaseRepository(HttpClient context)
        {
            _context = context;
        }

        public async Task<HttpResponseMessage> AddAsync(string uri,T entity)
        {
            return await _context.PostAsJsonAsync<T>(uri, entity);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string uri)
        {
           return await _context.DeleteAsync(uri);
        }

        public Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetResult(string uri)
        {
            JsonSerializerOptions options= new JsonSerializerOptions(); 

            Task<T[]?> c = _context.GetFromJsonAsync<T[]>(uri);
            return await c;
        }

        public async Task<T> GetResultByIdAsync(string uri)
        {
            Task<T?> c = _context.GetFromJsonAsync<T>(uri);
            return await c;
        }

        public  Task<string> GetResultSerialize(string uri)
        {
            return _context.GetStringAsync(uri);
        }

        public async Task<HttpResponseMessage> UpdateAsync(string uri, T entity)
        {
            return await _context.PutAsJsonAsync<T>(uri,entity);
        }
    }
}

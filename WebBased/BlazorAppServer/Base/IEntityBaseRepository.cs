using EBazarModels.Models;
using System.Linq.Expressions;

namespace BlazorAppServer.Base
{
    public interface IEntityBaseRepository<T> where T : IEntityBase,new()
    {
        Task<IEnumerable<T>> GetResult(string uri);
        //Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetResultByIdAsync(string uri);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties);
        Task AddAsync(T entity);
        Task<HttpResponseMessage> UpdateAsync(string uri, T entity);
        Task DeleteAsync(int id);
        Task<string> GetResultSerialize(string uri);
    }
}

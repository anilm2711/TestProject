using BlazorAppServer.Data;

namespace BlazorAppServer.Services
{
    public interface IMovieCustomService
    {
        Task<HttpResponseMessage> AddMovie(string uri, NewMovieVM entity);
    }
}

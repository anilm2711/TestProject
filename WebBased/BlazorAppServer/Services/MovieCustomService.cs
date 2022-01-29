using BlazorAppServer.Data;

namespace BlazorAppServer.Services
{
    public class MovieCustomService : IMovieCustomService
    {
        private readonly HttpClient _httpClient;

        public MovieCustomService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> AddMovie(string uri, NewMovieVM entity)
        {
            return await _httpClient.PostAsJsonAsync(uri, entity);
        }
    }
}

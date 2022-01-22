using BlazorAppServer.Base;
using EBazarModels.Models;

namespace BlazorAppServer.Services
{
    public class MovieService : EntityBaseRepository<Movie>, IMovieService
    {
        public MovieService(HttpClient httpClient) : base(httpClient)
        {

        }

    }
}

using EBazarModels.Models;

namespace BlazorAppServer.Data
{
    public class NewMovieDropdownsVM
    {
        public NewMovieDropdownsVM()
        {
            Producers = new List<Producer>();
            Cinemas = new List<Cinema>();
            Actors = new List<Actor>();
        }

        public IEnumerable<Producer> Producers { get; set; }
        public IEnumerable<Cinema> Cinemas { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
    }
}

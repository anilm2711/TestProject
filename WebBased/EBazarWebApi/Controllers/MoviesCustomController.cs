#nullable disable
using EBazarModels.Models;
using EBazarWebApi.Data;
using EBazarWebApi.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EBazarWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesCustomController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoviesCustomController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetMovieById(int id)
        {
            var movieDetails = await _context.Movies
                .Include("Cinema")
                .Include("Producer")
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            string json = JsonConvert.SerializeObject(movieDetails, Formatting.Indented, new JsonSerializerSettings
            { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

            if (movieDetails == null)
            {
                return NotFound();
            }

            return json;
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PutMovie(NewMovieVM data)
        {
            var newMovie = new Movie()
            {

                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId
            };

            try
            {
                _context.Movies.Add(newMovie);
                _context.SaveChanges();
                //await _context.Movies.AddAsync(newMovie);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }


            //Add Movie Actors

            try
            {
                foreach (var actorId in data.ActorIds)
                {
                    var newActorMovie = new Actor_Movie()
                    {
                        MovieId = newMovie.Id,
                        ActorId = actorId
                    };
                    _context.Actors_Movies.Add(newActorMovie);
                    _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return CreatedAtAction("GetMovie", new { id = newMovie.Id }, newMovie);


        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        [HttpPut]
        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbMovie != null)
            {
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.Name = data.Name;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
                await _context.SaveChangesAsync();
            }

            //Remove existing actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

    }
}

#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EBazarModels.Models;
using EBazarWebApi.Data;
using Newtonsoft.Json;
using EBazarWebApi.Data.ViewModels;

namespace EBazarWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public string GetMovies()
        {
            List<Movie> movie = _context.Movies.Include(e => e.Cinema).Include(x => x.Producer).ToList();
            string json = JsonConvert.SerializeObject(movie, Formatting.Indented, new JsonSerializerSettings 
            { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
            return json;
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetMovieById(int id)
        {
            var movieDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
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

        [Route("GetSingleMovieById/{id}/")]
        [HttpGet]
        public async Task<ActionResult<string>> GetSingleMovieById(int id)
        {
            var movieDetails = await _context.Movies.FirstOrDefaultAsync(n => n.Id == id);

            string json = JsonConvert.SerializeObject(movieDetails, Formatting.Indented, new JsonSerializerSettings
            { PreserveReferencesHandling = PreserveReferencesHandling.Objects });

            if (movieDetails == null)
            {
                return NotFound();
            }

            return json;
        }
        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        [Route("PostMovieVM/{id}/")]
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovieVM(int id,NewMovieVM data)
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
            await _context.Movies.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = newMovie.Id }, newMovie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}

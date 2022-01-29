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
            catch (Exception ex )
            {

                throw ex;
            }

            return CreatedAtAction("GetMovie", new { id = newMovie.Id }, newMovie);


        }
 
    }
}

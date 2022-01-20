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
using EBazarWebApi.Data.Base;
using EBazarWebApi.Data.Services;

namespace EBazarWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private ActorsService service;
        public ActorsController(AppDbContext context)
        {
            service = new ActorsService(context);
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<IEnumerable<Actor>> GetActors()
        {
            Task<IEnumerable<Actor>> x = service.GetAllAsync();
            return await x;

        }
        // GET: api/Actors/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Actor>> GetActor(int id)
        {
            try
            {
                var actor = await service.GetByIdAsync(id);
                if (actor == null)
                {
                    return NotFound();
                }
                return actor;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the Database");
            }
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutActor(int id, Actor actor)
        {
            if (id != actor.Id)
            {
                return BadRequest();
            }

            try
            {
                service.UpdateAsync(id, actor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the Database");
                }
            }

            return NoContent();
        }

        // POST: api/Actors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Actor>> PostActor(Actor actor)
        {
            try
            {
                if (actor == null)
                {
                    return BadRequest();
                }
                await service.AddAsync(actor);
                return CreatedAtAction(nameof(GetActor), new { id = actor.Id }, actor);
            }
            catch (Exception)
            {

                throw;
            }

        }

        // DELETE: api/Actors/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            await service.DeleteAsync(id);

            return NoContent();
        }

        private bool ActorExists(int id)
        {
            return service.IsExists(id);
        }
    }
}

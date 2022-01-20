using eBazar.Data;
using eBazar.Models;
using Microsoft.AspNetCore.Mvc;

namespace eBazar.Controllers
{
    public class ActorsController : Controller
    {
        private readonly AppDbContext _context;

        public ActorsController(AppDbContext context) => _context = context;
        public IActionResult Index()
        {
            IEnumerable<Actor> actors = _context.Actors;
            return View(actors);
        }
    }
}

using eBazar.Data;
using eBazar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eBazar.Controllers
{
    public class CinemasController : Controller
    {
        private readonly AppDbContext _context;

        public CinemasController(AppDbContext context)
        {
            _context = context;
        }
        public async Task <IActionResult> Index()
        {
            IEnumerable<Cinema> cinemaList = await _context.Cinemas.ToListAsync();
            return View();
        }
    }
}

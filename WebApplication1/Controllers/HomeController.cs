using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            TempData["Message"] = "Trying Data transfer";
            return RedirectToAction("GoToHome", "Home");
        }
        public IActionResult GoToHome()
        {
            ViewData["Time"] = DateTime.Now.ToString();
            ViewBag.Time=DateTime.Now.ToString();
            return View("MyHomePage");
        }
    }
}

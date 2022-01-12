using FnFManagement.Data;
using FnFManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FnFManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _db = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Person> peopleList = _db.People;
            return View(peopleList);
        }
        public IActionResult AddPerson()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPerson(Person person)
        {
            if (ModelState.IsValid)
            {
                if (person.PersonType == 0)
                    person.PersonType = 1;
                _db.People.Add(person);
                _db.SaveChanges();
                TempData["success"] = "Data added successfully.";
                return RedirectToAction("Index");
            }
            return View(person);
        }
        public IActionResult EditPerson(int PersionId)
        {
            Person person = _db.People.Find(PersionId);
            return View(person);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPerson(Person obj)
        {

            if (ModelState.IsValid)
            {
                if (obj.PersionId > 0)
                {
                    _db.People.Update(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Data updated successfully.";
                    return RedirectToAction("Index");
                }
            }
            return View(obj);
        }

        public IActionResult DeletePerson(int PersionId)
        {
            Person person = _db.People.Find(PersionId);
            return View(person);
        }

        [HttpPost,ActionName("DeletePerson")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int PersionId)
        {
            Person obj = null;
            if (ModelState.IsValid)
            {
                obj = _db.People.Find(PersionId);
                if (obj.PersionId > 0)
                {
                    _db.People.Remove(obj);
                    _db.SaveChanges();
                    TempData["success"] = "Data delete successfully.";
                    return RedirectToAction("Index");
                }
            }
            return View(obj);
        }

        public IActionResult Search()
        {
            string name = Request.Form["PersonName"];
            IEnumerable<Person> people = _db.People.Where(p => p.NameFirst == name);
            return View("Index", people);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
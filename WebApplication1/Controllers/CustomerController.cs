using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult AddCustomer()
        {
            return View("AddCustomer");
        }
        public IActionResult Submit(Customer obj)
        {
            //Customer obj = new Customer();
            //obj.CustomerName = Request.Form["CustomerName"];
            //obj.CustomerCode = Request.Form["CustomerCode"];
            return View("CustomerDetails", obj);
        }
    }
}

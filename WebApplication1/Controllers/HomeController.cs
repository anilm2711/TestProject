using Microsoft.AspNetCore.Mvc;
using Repository.EF.Factory;
using Repository.EF.Repositories;
using WebApplication1.DataAccessLibrary;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //Get All Record
            using (var context = new CustomerDbContext())
            using (var unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<CustomerModel> customerRepository = new Repository<CustomerModel>(context, unitOfWork);

                var getAllRecord = customerRepository.GetAll().ToList();
            }
            CustomerModel customerModel = new CustomerModel();
            CustomerDbContext customerDbContext = new CustomerDbContext();
            customerModel = customerDbContext.Customers.FirstOrDefault();
            return View(customerModel);
        }
        public IActionResult GoToHome()
        {
           
            return View("MyHomePage");
        }
    }
}

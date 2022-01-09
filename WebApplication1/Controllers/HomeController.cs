using Microsoft.AspNetCore.Mvc;
using Repository.EF.Factory;
using Repository.EF.Repositories;
using WebApplication1.DataAccessLibrary;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            using (var context = new CustomerDbContext())
            using (var unitOfWork = new UnitOfWork(context))
            {
                IRepositoryAsync<CustomerModel> customerRepository = new Repository<CustomerModel>(context, unitOfWork);
                List<CustomerModel> customerModelsList = customerRepository.GetAll().ToList();
                customerViewModel.customer = new CustomerModel();
                customerViewModel.customersList = customerModelsList;
            }
            return View("Index", customerViewModel);
        }
        //GET
        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCustomer(CustomerModel customer)
        {
            if (ModelState.IsValid)
            {
                using (var context = new CustomerDbContext())
                using (var unitOfWork = new UnitOfWork(context))
                {
                    IRepositoryAsync<CustomerModel> customerRepository = new Repository<CustomerModel>(context, unitOfWork);
                    customerRepository.Insert(customer);
                    unitOfWork.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(customer);
            }
        }
        //GET
        [HttpGet]
        public IActionResult EditCustomer(int id)
        {
            if(id==0)
            {
                return NotFound();
            }
            using(var context =new CustomerDbContext())
            {
                using(var UnitofWork=new UnitOfWork(context))
                {
                    IRepositoryAsync<CustomerModel> repositoryAsync = new Repository<CustomerModel>(context, UnitofWork);
                    var custModel = repositoryAsync.Find(id);
                    if (custModel == null)
                        return NotFound();
                    else
                    {
                        return View(custModel);
                    }
                }
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCustomer(CustomerModel customer)
        {
            if (ModelState.IsValid)
            {
                using (var context = new CustomerDbContext())
                using (var unitOfWork = new UnitOfWork(context))
                {
                    IRepositoryAsync<CustomerModel> customerRepository = new Repository<CustomerModel>(context, unitOfWork);
                    customerRepository.Insert(customer);
                    unitOfWork.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(customer);
            }
        }
    }
}

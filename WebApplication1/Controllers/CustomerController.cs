using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Repository.EF.Factory;
using Repository.EF.Repositories;
using WebApplication1.DataAccessLibrary;
using WebApplication1.Models;
using WebApplication1.ViewModel;

namespace WebApplication1.Controllers
{
    public class CustomerBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            HttpContext httpContext = bindingContext.HttpContext;
            string CustomerName = httpContext.Request.Form["CustomerName"];
            string CustomerCode = httpContext.Request.Form["CustomerCode"];
            Customer customer = new Customer() { CustomerName = CustomerName, CustomerCode = CustomerCode };
            bindingContext.Result = ModelBindingResult.Success(customer);
            return Task.CompletedTask;
        }
    }
    public class CustomerController : Controller
    {
        public IActionResult AddCustomer()
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
            return View("AddCustomer", customerViewModel);
        }
        public IActionResult Submit()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            if (ModelState.IsValid)
            {
                //CustomerModel custModel = new CustomerModel();
                //CustomerDbContext customerDbContext = new CustomerDbContext();
                //custModel.CustomerName = Request.Form["CustomerName"];
                //custModel.CustomerCode = Request.Form["CustomerCode"];
                //custModel.CustomerName = obj.CustomerName;
                //custModel.CustomerCode = obj.CustomerCode;
                //customerDbContext.Add(custModel);
                //customerDbContext.Database.EnsureCreated();
                //customerDbContext.SaveChanges();
                //return View("CustomerDetails", obj);

                var context = new CustomerDbContext();
                var unitOfWork = new UnitOfWork(context);
                IRepositoryAsync<CustomerModel> customerRepository = new Repository<CustomerModel>(context, unitOfWork);

                CustomerModel custModel = new CustomerModel();
                custModel.CustomerName = Request.Form["CustomerName"];
                custModel.CustomerCode = Request.Form["CustomerCode"];
                customerRepository.Insert(custModel);
                unitOfWork.SaveChanges();
                customerViewModel.customer = custModel;
                customerViewModel.customersList = customerRepository.GetAll().ToList();


                //Transactional Insert
                //try
                //{
                //    unitOfWork.BeginTransaction();
                //    var customer = new Customer
                //    {
                //        UniqueId = Guid.NewGuid(),
                //        ParentId = 1,
                //        Name = "Test",
                //        Status = 1,
                //        CreateDate = DateTime.Now
                //    };

                //    customerRepository.Insert(customer);
                //    unitOfWork.SaveChanges();
                //    unitOfWork.Commit();
                //}
                //catch (Exception ex)
                //{
                //    unitOfWork.Rollback();
                //}


                //Get All Record
                //var getAllRecord = customerRepository.GetAll().ToList();

                //Filter for paging record
                //var query = customerRepository.Filter(null, null, null, 1, 10).ToList();




                //Fluent query
                //IQueryFluent<CustomerModel> queryFluent = new QueryFluent<Customer>(new Repository<Customer>(context, unitOfWork));
                //int totalRecord = 0;
                //Fluent query paging record
                //var selectPaging = queryFluent.SelectPage(1, 10, out totalRecord).ToList();


                //IRepositoryAsync<UserRole> userRoleRepository = new Repository<UserRole>(context, unitOfWork);
            }
            return View("AddCustomer", customerViewModel);
        }

        public IActionResult Search()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            customerViewModel.customersList = new List<CustomerModel>();
            return View ("SearchCustomer",customerViewModel);
        }

        public IActionResult SearchCustomer()
        {
            var customerViewModel = new CustomerViewModel();
            var customerContext = new CustomerDbContext();
            UnitOfWork unitOfWork = new UnitOfWork(customerContext);
            IRepositoryAsync<CustomerModel> repository = new Repository<CustomerModel>(customerContext, unitOfWork);
            string custName = Request.Form["CustomerName"].ToString();
            List<CustomerModel> customerModels = repository.GetAll().Where(p=>p.CustomerName==custName).ToList();
            customerViewModel.customersList=customerModels;
            return View("SearchCustomer", customerViewModel);
        }
        public IActionResult Create()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            customerViewModel.customersList = new List<CustomerModel>();
            customerViewModel.customer = new CustomerModel();
            return View("AddCustomer", customerViewModel);
        }
        public IActionResult Delete(int id)
        {
            var customerViewModel = new CustomerViewModel();
            var customerContext = new CustomerDbContext();
            UnitOfWork unitOfWork = new UnitOfWork(customerContext);
            IRepositoryAsync<CustomerModel> repository = new Repository<CustomerModel>(customerContext, unitOfWork);
            repository.DeleteAsync(id);
            customerViewModel.customer = new CustomerModel();
            customerViewModel.customersList = new List<CustomerModel>();
            return View("AddCustomer", customerViewModel);
        }
        [HttpGet]
        public IActionResult getCustomers()
        {
            var customerViewModel = new CustomerViewModel();
            var customerContext = new CustomerDbContext();
            UnitOfWork unitOfWork = new UnitOfWork(customerContext);
            IRepositoryAsync<CustomerModel> repository = new Repository<CustomerModel>(customerContext, unitOfWork);
            customerViewModel.customer = new CustomerModel();
            customerViewModel.customersList =repository.GetAll().ToList();
            return Json(customerViewModel.customersList.ToList());
        }


    }
}

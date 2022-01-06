using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Repository.EF.Factory;
using Repository.EF.Repositories;
using WebApplication1.DataAccessLibrary;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CustomerBinder :IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            HttpContext httpContext= bindingContext.HttpContext;
            string CustomerName = httpContext.Request.Form["CustomerName"];
            string CustomerCode = httpContext.Request.Form["CustomerCode"];
            Customer customer=new Customer() { CustomerName=CustomerName, CustomerCode=CustomerCode};
            bindingContext.Result = ModelBindingResult.Success(customer);
            return Task.CompletedTask;
        }
    }
    public class CustomerController : Controller
    {
        public IActionResult AddCustomer()
        {
            return View("AddCustomer",new Customer());
        }
        public IActionResult Submit([ModelBinderAttribute(typeof(CustomerBinder))] Customer obj)
        {
            if (ModelState.IsValid)
            {
                //CustomerModel custModel = new CustomerModel();
                //CustomerDbContext customerDbContext = new CustomerDbContext();
                //custModel.CustomerName = obj.CustomerName;
                //custModel.CustomerCode = obj.CustomerCode;
                //customerDbContext.Add(custModel);
                //customerDbContext.Database.EnsureCreated();
                //customerDbContext.SaveChanges();
                //return View("CustomerDetails", obj);

                using (var context = new CustomerDbContext())
                using (var unitOfWork = new UnitOfWork(context))
                {
                    IRepositoryAsync<CustomerModel> customerRepository = new Repository<CustomerModel>(context, unitOfWork);

                    CustomerModel customerModel = new CustomerModel();
                    customerModel.CustomerName = obj.CustomerName;
                    customerModel.CustomerCode = obj.CustomerCode;
                    customerRepository.Insert(customerModel);
                    unitOfWork.SaveChanges();
                    return View("CustomerDetails", obj);
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
            }
            else
            {
                return View("AddCustomer",obj);
            }
        }
    }
}

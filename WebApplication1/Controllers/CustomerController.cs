using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
                CustomerModel custModel = new CustomerModel();
                CustomerDbContext customerDbContext = new CustomerDbContext();
                custModel.CustomerName = obj.CustomerName;
                custModel.CustomerCode = obj.CustomerCode;
                customerDbContext.Add(custModel);
                customerDbContext.Database.EnsureCreated();
                customerDbContext.SaveChanges();
                return View("CustomerDetails", obj);
            }
            else
            {
                return View("AddCustomer",obj);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CustomerBinder :IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            HttpContext httpContext= bindingContext.HttpContext;
            string CustomerName = httpContext.Request.Form["txtCustomerName"];
            string CustomerCode = httpContext.Request.Form["txtCustomerCode"];
            Customer customer=new Customer() { CustomerName=CustomerName, CustomerCode=CustomerCode};
            bindingContext.Result = ModelBindingResult.Success(customer);
            return Task.CompletedTask;
        }
    }
    public class CustomerController : Controller
    {
        public IActionResult AddCustomer()
        {
            return View("AddCustomer");
        }
        public IActionResult Submit([ModelBinderAttribute(typeof(CustomerBinder))] Customer obj)
        {
            //Customer obj = new Customer();
            //obj.CustomerName = Request.Form["CustomerName"];
            //obj.CustomerCode = Request.Form["CustomerCode"];
            return View("CustomerDetails",obj);
        }
    }
}

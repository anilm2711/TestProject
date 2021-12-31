using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers;

namespace WebApplication1.Models
{
    [ModelBinder(BinderType = typeof(CustomerBinder))]
    public class Customer
    {
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }

    }
}

using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Controllers;

namespace WebApplication1.Models
{
    [ModelBinder(BinderType = typeof(CustomerBinder))]
    public class Customer
    {
        [Required]
        public string? CustomerCode { get; set; }

        [Required]
        [StringLength(10)]
        public string? CustomerName { get; set; }

    }
}

using WebApplication1.DataAccessLibrary;
using WebApplication1.Models;
namespace WebApplication1.ViewModel
{
    public class CustomerViewModel
    {
        public CustomerModel customer { get; set; }
        public List<CustomerModel> customersList { get; set; }   
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessClassLibrary
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public string CustomerCode { get; set; }
        [Required]
        [StringLength(10)]
        public string? CustomerName { get; set; }

    }
}

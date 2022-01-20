using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EBazarModels.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }
    }
}

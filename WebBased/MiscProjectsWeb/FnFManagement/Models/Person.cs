using System.ComponentModel.DataAnnotations;

namespace FnFManagement.Models
{
    public class Person
    {
        [Key]
        public int PersionId { get; set; }
        [Required]
        [StringLength(15)]
        public string NameFirst { get; set; }
        [StringLength(5)]
        public string NameMiddle { get; set; }
                
        [Required]
        [StringLength(20)]
        public string NameLast { get; set; }

        [StringLength(25)]
        public string MobileNumber { get; set; }

        [StringLength(30)]
        public string AdhaarNumber { get; set; }

        [Required]
        [StringLength(30)]
        public string EmailId { get; set; }

        [Required]
        public int PersonType { get; set; }


    }
}

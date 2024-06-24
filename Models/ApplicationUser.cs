using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MVCKontorExpert.Models
{

    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [StringLength(255)] // Not required by default
        public string CompanyName { get; set; }

        [StringLength(50)] // Not required by default
        public string CVR { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [Required]
        [StringLength(20)]
        public string Zip { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }
    }
}

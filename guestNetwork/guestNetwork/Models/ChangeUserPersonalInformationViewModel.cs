using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace guestNetwork.Models
{
    public class ChangeUserPersonalInformationViewModel
    {
        [Required]
        [Display(Name = "Firstname")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Languages")]
        public IEnumerable<string> Languages { get; set; }
    }
}
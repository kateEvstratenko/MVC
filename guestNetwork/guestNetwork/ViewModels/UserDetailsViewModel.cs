using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Models;

namespace guestNetwork.ViewModels
{
    public class UserDetailsViewModel
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name = "Languages")]
        public IEnumerable<Language> Languages { get; set; }
    }
}
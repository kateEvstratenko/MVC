using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace guestNetwork.Models
{
    public class User : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        [Required]
        [MaxLength(GuestNetworkConstants.StringFieldLength, ErrorMessage = "{0} must be maximum of {1}")]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(GuestNetworkConstants.StringFieldLength, ErrorMessage = "{0} must be maximum of {1}")]
        public string Surname { get; set; }

        [Required]
        [MaxLength(GuestNetworkConstants.StringFieldLength, ErrorMessage = "{0} must be maximum of {1}")]
        public string Country { get; set; }

        [Required]
        [MaxLength(GuestNetworkConstants.StringFieldLength, ErrorMessage = "{0} must be maximum of {1}")]
        public string City { get; set; }

        public virtual ICollection<Language> Languages { get; set; }

        public virtual ICollection<Advertisement> Advertisements { get; set; }

        public virtual ICollection<Response> Responses { get; set; }

    }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }
}
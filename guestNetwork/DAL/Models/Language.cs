using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace guestNetwork.Models
{
    public class Language : Identity
    {
        [Required]
        [MaxLength(GuestNetworkConstants.StringFieldLength, ErrorMessage = "{0} must be maximum of {1}")]
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
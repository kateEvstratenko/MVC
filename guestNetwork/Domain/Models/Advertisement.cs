using System.ComponentModel.DataAnnotations;
using DAL;

namespace Domain.Models
{
    public class Advertisement : Identity
    {
        [Required]
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(GuestNetworkConstants.StringFieldLength, ErrorMessage = "{0} must be maximum of {1}")]
        public string Title { get; set; }
        
        public string MainImagePath { get; set; }
        
        [Required]
        [MaxLength(GuestNetworkConstants.ContentFieldLength, ErrorMessage = "{0} must be maximum of {1}")]
        public string Content { get; set; }

        [Required]
        public virtual Type Type { get; set; }

        public virtual User User { get; set; }

        public virtual Response Response { get; set; }
    }
}
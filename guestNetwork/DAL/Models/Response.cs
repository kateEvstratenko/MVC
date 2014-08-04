using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Response
    {
        [Key, ForeignKey("Advertisement")]
        [Required]
        public int AdvertisementId { get; set; }

        [Required]
        [MaxLength(GuestNetworkConstants.ContentFieldLength, ErrorMessage = "{0} must be maximum of {1}")]
        public string Message { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Advertisement Advertisement { get; set; }
    }
}
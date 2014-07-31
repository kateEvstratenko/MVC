using System.ComponentModel.DataAnnotations;


namespace guestNetwork.Models
{
    public class AdvertisementViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Title { get; set; }
        public string MainImagePath { get; set; }

        [Required]
        [MaxLength(GuestNetworkConstants.TextAreaLength, ErrorMessage = "{0} must be maximum of {1}")]
        public string Content { get; set; }
        [Required]
        public virtual Type Type { get; set; }
    }
}
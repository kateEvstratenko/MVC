using System.ComponentModel.DataAnnotations;

namespace guestNetwork.Models
{
    public class ResponseViewModel
    {
        public int AdvertisementId { get; set; }
        [Required]
        public string Message { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace guestNetwork.ViewModels
{
    public class ResponseViewModel
    {
        public int AdvertisementId { get; set; }
        [Required]
        public string Message { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int AdvertisementUserId { get; set; }
    }
}
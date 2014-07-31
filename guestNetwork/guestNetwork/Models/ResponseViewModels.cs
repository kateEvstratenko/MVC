using System.ComponentModel.DataAnnotations;

namespace guestNetwork.Models
{
    public class ResponseCreateViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
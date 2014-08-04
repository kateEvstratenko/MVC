using System.ComponentModel.DataAnnotations;

namespace guestNetwork.ViewModels
{
    public enum AdvertisementViewType
    {
        All,
        Invite,
        Find
    }
    public class FilterAdvertisementViewModel
    {
        [Display(Name = "Type: ")]
        public AdvertisementViewType AdvertisementViewType { get; set; }
        [Display(Name = "Show only active Advertisements: ")]
        public bool OnlyActiveAdvertisements { get; set; }
    }
}
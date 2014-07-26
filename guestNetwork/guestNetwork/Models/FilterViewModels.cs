using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace guestNetwork.Models
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
        public AdvertisementViewType advertisementViewType { get; set; }
        [Display(Name = "Show only active Advertisements")]
        public bool onlyActiveAdvertisements { get; set; }
    }
}
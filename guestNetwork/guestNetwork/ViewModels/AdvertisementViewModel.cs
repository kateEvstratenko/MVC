﻿using System.ComponentModel.DataAnnotations;
using DAL;
using DAL.Models;


namespace guestNetwork.ViewModels
{
    public class AdvertisementViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string MainImagePath { get; set; }

        [Required]
        [MaxLength(GuestNetworkConstants.ContentFieldLength, ErrorMessage = "{0} must be maximum of {1}")]
        public string Content { get; set; }

        [Required]
        public virtual Type Type { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

    }
}
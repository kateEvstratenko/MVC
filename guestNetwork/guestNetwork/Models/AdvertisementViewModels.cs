﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace guestNetwork.Models
{
    public class AdvertisementViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Title { get; set; }
        public string mainImagePath { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public virtual Type Type { get; set; }
    }
}
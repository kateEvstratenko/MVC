using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace guestNetwork.Models
{
    public enum Type
    {
        Invite,
        Find
    }

    public class Advertisement : Identity
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string MainImagePath { get; set; }
        public string Content { get; set; }
        public virtual Type Type { get; set; }
        public virtual User User { get; set; }
        public virtual Response Response { get; set; }
    }
}
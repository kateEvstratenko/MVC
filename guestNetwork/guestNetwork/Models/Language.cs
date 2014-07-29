using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace guestNetwork.Models
{
    public class Language : Identity
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
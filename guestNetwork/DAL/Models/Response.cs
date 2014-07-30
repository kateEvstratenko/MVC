using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace guestNetwork.Models
{
    public class Response : Identity
    {
        [Key, ForeignKey("Advertisement"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Advertisement Advertisement { get; set; }
    }
}
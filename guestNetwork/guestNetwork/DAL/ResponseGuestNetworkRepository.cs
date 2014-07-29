using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using guestNetwork.Models;

namespace guestNetwork.DAL
{
    public class ResponseGuestNetworkRepository : GuestNetworkRepository<Response>
    {
        public ResponseGuestNetworkRepository(IApplicationDbContext context) : base(context, x => x.Responses)
        {
           
        }
    }
}
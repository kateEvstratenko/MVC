using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using guestNetwork.Models;

namespace guestNetwork.DAL
{
    public class LanguageGuestNetworkRepository: GuestNetworkRepository<Language>
    {
        public LanguageGuestNetworkRepository(IApplicationDbContext context)
            : base(context, x => x.Languages)
        {

        }
    }
}
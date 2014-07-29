﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using guestNetwork.Models;

namespace guestNetwork.DAL
{
    public class AdvertisementGuestNetworkRepository: GuestNetworkRepository<Advertisement>
    {
        public AdvertisementGuestNetworkRepository(IApplicationDbContext context)
            : base(context, x => x.Advertisements)
        {

        }
    }
}
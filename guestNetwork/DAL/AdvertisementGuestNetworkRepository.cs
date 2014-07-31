using guestNetwork.Models;

namespace guestNetwork
{
    public class AdvertisementGuestNetworkRepository: GuestNetworkRepository<Advertisement>
    {
        public AdvertisementGuestNetworkRepository(IApplicationDbContext context)
            : base(context, x => x.Advertisements)
        {

        }
    }
}
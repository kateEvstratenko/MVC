using guestNetwork.Models;

namespace guestNetwork
{
    public class UserGuestNetworkRepository : GuestNetworkRepository<User>
    {
        public UserGuestNetworkRepository(IApplicationDbContext context)
            : base(context, x => x.Users)
        {
           
        }
    }
}

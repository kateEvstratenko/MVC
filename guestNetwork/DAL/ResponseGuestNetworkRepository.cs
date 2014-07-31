using guestNetwork.Models;

namespace guestNetwork
{
    public class ResponseGuestNetworkRepository : GuestNetworkRepository<Response>
    {
        public ResponseGuestNetworkRepository(IApplicationDbContext context) : base(context, x => x.Responses)
        {
           
        }
    }
}
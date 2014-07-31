using guestNetwork.Models;

namespace guestNetwork
{
    public class LanguageGuestNetworkRepository: GuestNetworkRepository<Language>
    {
        public LanguageGuestNetworkRepository(IApplicationDbContext context)
            : base(context, x => x.Languages)
        {

        }
    }
}
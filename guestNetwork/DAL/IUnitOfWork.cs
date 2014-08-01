using guestNetwork.Models;

namespace guestNetwork
{
    public interface IUnitOfWork
    {
        void Commit();
        IGuestNetworkRepository<User> UserRepository { get; }
        IGuestNetworkRepository<Advertisement> AdvertisementRepository{ get; }
        IGuestNetworkRepository<Language> LanguageRepository{ get; }
        IGuestNetworkRepository<Response> ResponseRepository{ get; }
        IApplicationDbContext Context { get; }
    }
}

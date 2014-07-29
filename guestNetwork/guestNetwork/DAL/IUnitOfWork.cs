using guestNetwork.Models;

namespace guestNetwork.DAL
{
    public interface IUnitOfWork
    {
        void Save();
        IGuestNetworkRepository<User> UserRepository { get; }
        IGuestNetworkRepository<Advertisement> AdvertisementRepository{ get; }
        IGuestNetworkRepository<Language> LanguageRepository{ get; }
        IGuestNetworkRepository<Response> ResponseRepository{ get; }
        IApplicationDbContext Context { get; }
    }
}

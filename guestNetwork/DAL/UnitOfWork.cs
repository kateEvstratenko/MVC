using guestNetwork.Models;

namespace guestNetwork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext context;
        private readonly IGuestNetworkRepository<User> userRepository;
        private readonly IGuestNetworkRepository<Advertisement> advertisementRepository;
        private readonly IGuestNetworkRepository<Language> languageRepository;
        private readonly IGuestNetworkRepository<Response> responseRepository;

        public UnitOfWork(IGuestNetworkRepository<User> userInstance,
            IGuestNetworkRepository<Advertisement> advertisementInstance,
            IGuestNetworkRepository<Language> languageInstance,
            IGuestNetworkRepository<Response> responseInstance,
            IApplicationDbContext contextInstance)
        {
            context = contextInstance;
            userRepository = userInstance;
            advertisementRepository = advertisementInstance;
            languageRepository = languageInstance;
            responseRepository = responseInstance;
        }
        public IGuestNetworkRepository<User> UserRepository
        {
            get { return userRepository; }
        }

        public IGuestNetworkRepository<Advertisement> AdvertisementRepository
        {
            get { return advertisementRepository; }
        }

        public IGuestNetworkRepository<Language> LanguageRepository
        {
            get { return languageRepository; }
        }

        public IGuestNetworkRepository<Response> ResponseRepository
        {
            get { return responseRepository; }
        }

        public IApplicationDbContext Context
        {
            get { return context; }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
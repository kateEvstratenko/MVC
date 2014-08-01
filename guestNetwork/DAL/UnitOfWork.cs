using System.Data.Entity;
using guestNetwork;
using guestNetwork.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class UnitOfWork : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IUnitOfWork
    {
        public UnitOfWork()
            : base("DefaultConnection")
        {

        }

        private GuestNetworkRepository<User> _userRepo;
        private GuestNetworkRepository<Advertisement> _advertismentRepo;
        private GuestNetworkRepository<Language> _languageRepo;
        private GuestNetworkRepository<Response> _responseRepo;

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Response> Responses { get; set; }


        public IGuestNetworkRepository<User> UserRepository
        {
            get { return _userRepo ?? (_userRepo = new GuestNetworkRepository<User>(Users)); }
        }

        public IGuestNetworkRepository<Advertisement> AdvertisementRepository
        {
            get { return _advertismentRepo ?? (_advertismentRepo = new GuestNetworkRepository<Advertisement>(Advertisements)); }
        }

        public IGuestNetworkRepository<Language> LanguageRepository
        {
            get { return _languageRepo ?? (_languageRepo = new GuestNetworkRepository<Language>(Languages)); }
        }

        public IGuestNetworkRepository<Response> ResponseRepository
        {
            get { return _responseRepo ?? (_responseRepo = new GuestNetworkRepository<Response>(Responses)); }
        }

        public void Commit()
        {
            SaveChanges();
        }
    }
}
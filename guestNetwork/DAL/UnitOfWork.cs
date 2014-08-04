using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL
{
    public class UnitOfWork : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IUnitOfWork
    {
        public UnitOfWork()
            : base("DefaultConnection")
        {

        }

        private GuestNetworkRepository<User> _userRepository;
        private GuestNetworkRepository<Advertisement> _advertismentRepository;
        private GuestNetworkRepository<Language> _languageRepository;
        private GuestNetworkRepository<Response> _responseRepository;

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Response> Responses { get; set; }


        public IGuestNetworkRepository<User> UserRepository
        {
            get { return _userRepository ?? (_userRepository = new GuestNetworkRepository<User>(Users, this)); }
        }

        public IGuestNetworkRepository<Advertisement> AdvertisementRepository
        {
            get { return _advertismentRepository ?? (_advertismentRepository = new GuestNetworkRepository<Advertisement>(Advertisements, this)); }
        }

        public IGuestNetworkRepository<Language> LanguageRepository
        {
            get { return _languageRepository ?? (_languageRepository = new GuestNetworkRepository<Language>(Languages, this)); }
        }

        public IGuestNetworkRepository<Response> ResponseRepository
        {
            get { return _responseRepository ?? (_responseRepository = new GuestNetworkRepository<Response>(Responses, this)); }
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }
    }
}
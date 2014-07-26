using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using guestNetwork.Models;

namespace guestNetwork.DAL
{
    public class UnitOfWork
    {
        public ApplicationDbContext context = new ApplicationDbContext();
        private GuestNetworkRepository<User> userRepository;
        private GuestNetworkRepository<Advertisement> advertisementRepository;
        private GuestNetworkRepository<Language> languageRepository;
        private GuestNetworkRepository<Response> responseRepository;

        public GuestNetworkRepository<User> UserRepository
        {
            get 
            {
                if (userRepository == null)
                {
                    userRepository = new GuestNetworkRepository<User>(context);
                }
                return userRepository;
            }
        }

        public GuestNetworkRepository<Advertisement> AdvertisementRepository
        {
            get 
            { 
                if (advertisementRepository == null)
                {
                    advertisementRepository = new GuestNetworkRepository<Advertisement>(context);
                }
                return advertisementRepository;
            }
        }

        public GuestNetworkRepository<Language> LanguageRepository
        {
            get
            {
                if (languageRepository == null)
                {
                    languageRepository = new GuestNetworkRepository<Language>(context);
                }
                return languageRepository;
            }
        }

        public GuestNetworkRepository<Response> ResponseRepository
        {
            get
            {
                if (responseRepository == null)
                {
                    responseRepository = new GuestNetworkRepository<Response>(context);
                }
                return responseRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
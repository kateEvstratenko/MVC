using AutoMapper;

namespace guestNetwork.Models
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            ConfigureAdvertisementMapping();
            //ConfigurePostMapping();
        }

        private static void ConfigureAdvertisementMapping()
        {
            //Mapper.CreateMap<User, UserViewModel>();
            Mapper.CreateMap<Advertisement, AdvertisementViewModel>();
        } 
    }
}

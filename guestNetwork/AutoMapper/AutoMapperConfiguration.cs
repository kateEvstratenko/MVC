using guestNetwork.Models;

namespace AutoMapper
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

using AutoMapper;
using DAL.Models;
using guestNetwork.ViewModels;

namespace guestNetwork
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            ConfigureAdvertisementMapping();
            ConfigureUserMapping();
            ConfigureResponseMapping();
        }

        private static void ConfigureAdvertisementMapping()
        {
            Mapper.CreateMap<Advertisement, AdvertisementViewModel>()
                .AfterMap((advertisement, model) =>
                {
                    model.UserName = advertisement.User.UserName;
                });

            Mapper.CreateMap<AdvertisementViewModel, Advertisement>()
            .AfterMap((model, advertisement) =>
            {
                advertisement.User = null;
                advertisement.Response = null;
            });
        }

        private static void ConfigureUserMapping()
        {
            Mapper.CreateMap<RegisterViewModel, User>()
                .ForMember(x => x.Languages, u => u.Ignore());

            Mapper.CreateMap<User, ChangeUserPersonalInformationViewModel>()
                .ForMember(x => x.Languages, u => u.Ignore());

            Mapper.CreateMap<ChangeUserPersonalInformationViewModel, User>()
                .ForMember(x => x.Languages, u => u.Ignore());

            Mapper.CreateMap<User, UserDetailsViewModel>();
        }

        private static void ConfigureResponseMapping()
        {
            Mapper.CreateMap<Response, ResponseViewModel>()
                .AfterMap((response, model) =>
                {
                    model.UserName = response.User.UserName;
                    model.AdvertisementUserId = response.Advertisement.UserId;
                });

            Mapper.CreateMap<ResponseViewModel, Response>();
        }
    }
}
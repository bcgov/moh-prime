using AutoMapper;

using Prime.Models;

namespace Prime.ViewModels.Profiles
{
    public class BannerMappingProfile : Profile
    {
        public BannerMappingProfile()
        {
            CreateMap<Banner, BannerDisplayViewModel>();
            CreateMap<Banner, BannerViewModel>();
        }
    }
}

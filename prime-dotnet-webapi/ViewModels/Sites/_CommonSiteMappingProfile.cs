using AutoMapper;
using System.Linq;

using Prime.Models;
using Prime.ViewModels.Sites;

namespace Prime.ViewModels.Profiles
{
    public class CommonSiteMappingProfile : Profile
    {
        public CommonSiteMappingProfile()
        {
            CreateMap<BusinessDay, BusinessDayViewModel>()
                .ReverseMap();
            CreateMap<RemoteUser, RemoteUserViewModel>()
                .ReverseMap();
            CreateMap<RemoteUserCertification, RemoteUserCertificationViewModel>()
                .ReverseMap();

            CreateMap<RemoteUserCertification, RemoteAccessSearchDto>()
                .ForMember(dest => dest.SiteAddress, opt => opt.MapFrom(src => src.RemoteUser.Site.PhysicalAddress))
                .ForMember(dest => dest.SiteDoingBusinessAs, opt => opt.MapFrom(src => src.RemoteUser.Site.DoingBusinessAs))
                .ForMember(dest => dest.SiteId, opt => opt.MapFrom(src => src.RemoteUser.Site.Id))
                .ForMember(dest => dest.CommunityVendorCodes, opt => opt.MapFrom(src => (src.RemoteUser.Site as CommunitySite).SiteVendors.Select(x => x.VendorCode)))
                .ForMember(dest => dest.HealthAuthorityVendorCode, opt => opt.MapFrom(src => (src.RemoteUser.Site as HealthAuthoritySite).HealthAuthorityVendor.VendorCode));
            CreateMap<RemoteAccessSearchDto, RemoteAccessSearchViewModel>();
        }
    }
}

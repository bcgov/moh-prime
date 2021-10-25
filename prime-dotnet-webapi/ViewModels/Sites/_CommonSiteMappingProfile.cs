using System.Linq;
using AutoMapper;

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

            // TODO this must be tested and adjusted
            // CreateMap<RemoteUser, RemoteAccessSearchViewModel>()
            //     .IncludeMembers(user => user.Site);
            // CreateMap<CommunitySite, RemoteAccessSearchViewModel>()
            //     .ForMember(dest => dest.VendorCodes, opt => opt.MapFrom(src => src.SiteVendors.Select(x => x.VendorCode)));
            // CreateMap<V2HealthAuthoritySite, RemoteAccessSearchViewModel>()
            //     .ForMember(dest => dest.VendorCodes, opt => opt.MapFrom(src => new[] { src.HealthAuthorityVendor.VendorCode }));
        }
    }
}

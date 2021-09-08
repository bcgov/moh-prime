using System.Linq;
using AutoMapper;

using Prime.Models;
using Prime.Models.HealthAuthorities;
using Prime.ViewModels.Parties;
using Prime.ViewModels.HealthAuthorities;

namespace Prime.ViewModels.Profiles
{
    public class HealthAuthorityMappingProfile : Profile
    {
        public HealthAuthorityMappingProfile()
        {
            IQueryable<int> underReviewIds = null;
            CreateMap<HealthAuthorityOrganization, HealthAuthorityListViewModel>()
                .ForMember(dest => dest.HasUnderReviewUsers, opt => opt.MapFrom(src => underReviewIds.Contains(src.Id)));
            CreateMap<HealthAuthorityOrganization, HealthAuthorityViewModel>()
                .ForMember(dest => dest.CareTypes, opt => opt.MapFrom(src => src.CareTypes.Select(x => x.CareType)))
                .ForMember(dest => dest.VendorCodes, opt => opt.MapFrom(src => src.Vendors.Select(x => x.VendorCode)))
                .ForMember(dest => dest.TechnicalSupports, opt => opt.MapFrom(src => src.TechnicalSupports.Select(x => x.Contact)))
                .ForMember(dest => dest.PharmanetAdministrators, opt => opt.MapFrom(src => src.PharmanetAdministrators.Select(x => x.Contact)));

            CreateMap<PrivacyOffice, PrivacyOfficeViewModel>()
                .ForMember(dest => dest.PrivacyOfficer, opt => opt.MapFrom(src => src.HealthAuthorityOrganization.PrivacyOfficers.Select(x => x.Contact).SingleOrDefault()))
                .ReverseMap();
            CreateMap<Contact, PrivacyOfficerViewModel>();

            CreateMap<AuthorizedUser, AuthorizedUserViewModel>()
                .IncludeMembers(src => src.Party);
            CreateMap<Party, AuthorizedUserViewModel>();
        }
    }
}

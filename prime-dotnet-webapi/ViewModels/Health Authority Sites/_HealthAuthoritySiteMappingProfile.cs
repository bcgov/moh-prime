using AutoMapper;
using System.Linq;

using Prime.Models;
using Prime.Models.HealthAuthorities;
using Prime.ViewModels.HealthAuthoritySites.Internal;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteMappingProfile : Profile
    {
        public HealthAuthoritySiteMappingProfile()
        {
            CreateMap<HealthAuthoritySite, HealthAuthoritySiteViewModel>();
            CreateMap<HealthAuthoritySite, HealthAuthoritySiteAdminListViewModel>()
                .ForMember(dest => dest.AuthorizedUserName, opt => opt.MapFrom(src => $"{src.AuthorizedUser.Party.FirstName} {src.AuthorizedUser.Party.LastName}"))
                .ForMember(dest => dest.AuthorizedUserEmail, opt => opt.MapFrom(src => src.AuthorizedUser.Party.Email))
                .ForMember(dest => dest.HealthAuthorityName, opt => opt.MapFrom(src => src.HealthAuthorityOrganization.Name))
                .ForMember(dest => dest.AdjudicatorIdir, opt => opt.MapFrom(src => src.AdjudicatorId != null ? src.Adjudicator.IDIR : null));
            CreateMap<HealthAuthoritySite, HealthAuthoritySiteListViewModel>();
            CreateMap<HealthAuthorityPharmanetAdministrator, HealthAuthoritySite>();

            CreateMap<HealthAuthoritySite, SiteSelectionDto>();
            CreateMap<HealthAuthoritySiteUpdateModel, SiteSelectionDto>();
            CreateMap<HealthAuthorityOrganization, HealthAuthoritySelectionDto>()
                .ForMember(dest => dest.VendorIds, opt => opt.MapFrom(src => src.Vendors.Select(x => x.Id)))
                .ForMember(dest => dest.CareTypeIds, opt => opt.MapFrom(src => src.CareTypes.Select(x => x.Id)))
                .ForMember(dest => dest.PharmanetAdministratorIds, opt => opt.MapFrom(src => src.PharmanetAdministrators.Select(x => x.Id)))
                .ForMember(dest => dest.TechnicalSupportIds, opt => opt.MapFrom(src => src.TechnicalSupports.Select(x => x.Id)));
        }
    }
}

using AutoMapper;
using Prime.Models;
using Prime.Models.HealthAuthorities;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteMappingProfile : Profile
    {
        public HealthAuthoritySiteMappingProfile()
        {
            CreateMap<HealthAuthoritySite, HealthAuthoritySiteViewModel>();
            CreateMap<HealthAuthorityPharmanetAdministrator, HealthAuthoritySite>();
            CreateMap<V2HealthAuthoritySite, V2HealthAuthoritySiteViewModel>();
        }
    }
}

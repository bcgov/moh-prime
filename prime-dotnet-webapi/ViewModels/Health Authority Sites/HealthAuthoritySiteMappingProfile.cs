using AutoMapper;
using Prime.Models.HealthAuthorities;
using Prime.ViewModels.HealthAuthoritySites;

public class HealthAuthoritySiteMappingProfile : Profile
{
    public HealthAuthoritySiteMappingProfile()
    {
        CreateMap<HealthAuthoritySite, HealthAuthoritySiteViewModel>();
        CreateMap<HealthAuthoritySiteInfoViewModel, HealthAuthoritySite>();
    }
}

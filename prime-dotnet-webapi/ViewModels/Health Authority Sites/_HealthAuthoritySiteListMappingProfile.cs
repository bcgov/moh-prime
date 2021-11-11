using AutoMapper;

using Prime.Models;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteListMappingProfile : Profile
    {
        public HealthAuthoritySiteListMappingProfile()
        {
            CreateMap<HealthAuthoritySite, HealthAuthoritySiteListViewModel>()
                .ForMember(dest => dest.AdjudicatorIdir, opt => opt.MapFrom(src => src.Adjudicator.IDIR));
        }
    }
}

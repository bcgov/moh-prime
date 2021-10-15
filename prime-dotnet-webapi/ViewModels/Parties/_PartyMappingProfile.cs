using AutoMapper;

using Prime.Models;
using Prime.ViewModels.Parties;

namespace Prime.ViewModels.Profiles
{
    public class PartyMappingProfile : Profile
    {
        public PartyMappingProfile()
        {
            CreateMap<GisEnrolment, GisViewModel>()
                .IncludeMembers(g => g.Party);
            CreateMap<Party, GisViewModel>();
            CreateMap<Party, SatViewModel>();
            CreateMap<PartyCertificationViewModel, PartyCertification>();
        }
    }
}

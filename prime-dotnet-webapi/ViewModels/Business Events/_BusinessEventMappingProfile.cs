using AutoMapper;
using Prime.Models;

namespace Prime.ViewModels.Profiles
{
    public class BusinessEventMappingProfile : Profile
    {
        public BusinessEventMappingProfile()
        {
            CreateMap<BusinessEvent, SiteBusinessEventViewModel>()
                .ForMember(dest => dest.PartyName, opt => opt.MapFrom(src => src.Party.FirstName + ' ' + src.Party.LastName));
        }
    }
}

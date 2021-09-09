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
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Party.UserId))
                .ForMember(dest => dest.HPDID, opt => opt.MapFrom(src => src.Party.HPDID))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Party.FirstName))
                .ForMember(dest => dest.GivenNames, opt => opt.MapFrom(src => src.Party.GivenNames))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Party.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Party.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Party.Phone));
        }
    }
}

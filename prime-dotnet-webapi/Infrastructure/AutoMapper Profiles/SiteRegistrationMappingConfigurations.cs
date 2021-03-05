using System.Linq;
using AutoMapper;

using Prime.Models;
using Prime.ViewModels;

namespace Prime.Infrastructure.AutoMapperProfiles
{
    public class SiteRegistrationMappingConfigurations : Profile
    {
        public SiteRegistrationMappingConfigurations()
        {
            CreateMap<Organization, OrganizationListViewModel>()
                .ForMember(dest => dest.HasAcceptedAgreement, opt => opt.MapFrom(src => src.Agreements.Any(a => a.AcceptedDate.HasValue)))
                .ForMember(dest => dest.HasSubmittedSite, opt => opt.MapFrom(src => src.Sites.Any(s => s.SubmittedDate.HasValue)));

            CreateMap<Site, SiteListViewModel>()
                .ForMember(dest => dest.AdjudicatorIdir, opt => opt.MapFrom(src => src.Adjudicator.IDIR))
                .ForMember(dest => dest.RemoteUserCount, opt => opt.MapFrom(src => src.RemoteUsers.Count));

            CreateMap<SiteRegistrationNote, SiteRegistrationNoteViewModel>();
        }
    }
}

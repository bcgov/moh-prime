using System.Linq;
using AutoMapper;

using Prime.Models;

namespace Prime.ViewModels.Profiles
{
    public class SiteMappingProfile : Profile
    {
        public SiteMappingProfile()
        {
            int? careSettingCode = null;
            CreateMap<Organization, OrganizationListViewModel>()
                .ForMember(dest => dest.HasAcceptedAgreement, opt => opt.MapFrom(src => src.Agreements.Any(a => a.AcceptedDate.HasValue)))
                .ForMember(dest => dest.HasSubmittedSite, opt => opt.MapFrom(src => src.Sites.Any(s => s.SubmittedDate.HasValue)))
                .ForMember(dest => dest.Sites, opt => opt.MapFrom(src => src.Sites.Where(s => careSettingCode == null || s.CareSettingCode == careSettingCode)));
            CreateMap<CommunitySite, CommunitySiteListViewModel>()
                .ForMember(dest => dest.AdjudicatorIdir, opt => opt.MapFrom(src => src.Adjudicator.IDIR))
                .ForMember(dest => dest.RemoteUserCount, opt => opt.MapFrom(src => src.RemoteUsers.Count));

            CreateMap<BusinessLicence, BusinessLicence>();

            CreateMap<SiteRegistrationNote, SiteRegistrationNoteViewModel>();

            CreateMap<SiteSubmissionViewModel, CommunitySiteUpdateModel>();
            CreateMap<SiteBusinessLicenceViewModel, BusinessLicence>();

            CreateMap<IndividualDeviceProvider, IndividualDeviceProviderViewModel>();
            CreateMap<IndividualDeviceProviderChangeModel, IndividualDeviceProvider>();
        }
    }
}

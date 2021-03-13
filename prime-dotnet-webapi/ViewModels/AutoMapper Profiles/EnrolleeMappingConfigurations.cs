using System.Linq;
using AutoMapper;

using Prime.Models;
using Prime.ViewModels;
using Prime.DTOs.AgreementEngine;

namespace Prime.Infrastructure.AutoMapperProfiles
{
    public class EnrolleeMappingConfigurations : Profile
    {
        public EnrolleeMappingConfigurations()
        {
            CreateMap<EnrolleeCreateModel, Enrollee>();

            IQueryable<int> newestAgreementIds = null;
            CreateMap<Enrollee, EnrolleeListViewModel>()
                .ForMember(dest => dest.CurrentStatusCode, opt => opt.MapFrom(src => src.CurrentStatus.StatusCode))
                .ForMember(dest => dest.AdjudicatorIdir, opt => opt.MapFrom(src => src.Adjudicator.IDIR))
                .ForMember(dest => dest.HasNewestAgreement, opt => opt.MapFrom(src => newestAgreementIds.Any(n => n == src.CurrentAgreementId)))
                .ForMember(dest => dest.RemoteAccess, opt => opt.MapFrom(src => src.EnrolleeRemoteUsers.Any()))
                .ForMember(dest => dest.CareSettingCodes, opt => opt.MapFrom(src => src.EnrolleeCareSettings.Select(ecs => ecs.CareSettingCode)));

            CreateMap<Enrollee, EnrolleeViewModel>()
                .ForMember(dest => dest.CurrentStatusCode, opt => opt.MapFrom(src => src.CurrentStatus.StatusCode))
                .ForMember(dest => dest.AdjudicatorIdir, opt => opt.MapFrom(src => src.Adjudicator.IDIR))
                .AfterMap((src, dest) => dest.IsRegulatedUser = src.IsRegulatedUser());

            CreateMap<Enrollee, EnrolleeDemographicViewModel>();

            CreateMap<Enrollee, EnrolleeOverviewViewModel>()
                .ForMember(dest => dest.CareSettings, opt => opt.MapFrom(src => src.EnrolleeCareSettings.Select(x => x.CareSettingCode)))
                .ForMember(dest => dest.Jobs, opt => opt.MapFrom(src => src.Jobs.Select(x => x.Title)));

            CreateMap<EnrolleeNote, EnrolleeNoteViewModel>();

            CreateMap<Enrollee, AgreementEngineDto>()
                .ForMember(dest => dest.CareSettingCodes, opt => opt.MapFrom(src => src.EnrolleeCareSettings.Select(ecs => ecs.CareSettingCode)));

            CreateMap<Certification, CertificationDto>();

            CreateMap<Certification, CertificationViewModel>();

            CreateMap<OboSite, OboSiteViewModel>();

            CreateMap<SelfDeclaration, SelfDeclarationViewModel>();
        }
    }
}

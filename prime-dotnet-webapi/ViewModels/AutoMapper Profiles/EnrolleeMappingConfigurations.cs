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

            CreateMap<Enrollee, EnrolleeDemographicViewModel>()
                .ForMember(dest => dest.VerifiedAddress, opt => opt.MapFrom(src => src.Addresses.OfType<VerifiedAddress>().SingleOrDefault()))
                .ForMember(dest => dest.PhysicalAddress, opt => opt.MapFrom(src => src.Addresses.OfType<PhysicalAddress>().SingleOrDefault()))
                .ForMember(dest => dest.MailingAddress, opt => opt.MapFrom(src => src.Addresses.OfType<MailingAddress>().SingleOrDefault()));

            CreateMap<EnrolleeNote, EnrolleeNoteViewModel>();

            CreateMap<Enrollee, AgreementEngineDto>()
                .ForMember(dest => dest.CareSettingCodes, opt => opt.MapFrom(src => src.EnrolleeCareSettings.Select(ecs => ecs.CareSettingCode)));

            CreateMap<Certification, CertificationDto>();

            CreateMap<SelfDeclaration, SelfDeclarationViewModel>();
        }
    }
}

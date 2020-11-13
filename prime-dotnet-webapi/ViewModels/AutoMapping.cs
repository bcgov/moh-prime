using System.Linq;
using AutoMapper;

using Prime.Models;
using Prime.ViewModels;
using Prime.DTOs.AgreementEngine;

/**
 * Automapper Documentation
 * @see https://docs.automapper.org/en/stable
 */
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Organization, OrganizationListViewModel>();
        CreateMap<Site, SiteListViewModel>()
            .ForMember(dest => dest.AdjudicatorIdir, opt => opt.MapFrom(src => src.Adjudicator.IDIR))
            .ForMember(dest => dest.RemoteUserCount, opt => opt.MapFrom(src => src.RemoteUsers.Count));

        CreateMap<EnrolleeCreateModel, Enrollee>();

        IQueryable<int> newestAgreementIds = null;
        CreateMap<Enrollee, EnrolleeListViewModel>()
            .ForMember(dest => dest.CurrentStatusCode, opt => opt.MapFrom(src => src.CurrentStatus.StatusCode))
            .ForMember(dest => dest.AdjudicatorIdir, opt => opt.MapFrom(src => src.Adjudicator.IDIR))
            .ForMember(dest => dest.HasNewestAgreement, opt => opt.MapFrom(src => newestAgreementIds.Any(n => n == src.CurrentAgreementId)))
            .ForMember(dest => dest.RemoteAccess, opt => opt.MapFrom(src => src.EnrolleeRemoteUsers.Any()));

        CreateMap<Enrollee, EnrolleeViewModel>()
            .ForMember(dest => dest.CurrentStatusCode, opt => opt.MapFrom(src => src.CurrentStatus.StatusCode))
            .ForMember(dest => dest.AdjudicatorIdir, opt => opt.MapFrom(src => src.Adjudicator.IDIR))
            .AfterMap((src, dest) => dest.IsRegulatedUser = src.IsRegulatedUser());

        CreateMap<Agreement, AgreementViewModel>()
            .ForMember(dest => dest.SignedAgreementDocumentGuid, opt =>
            {
                opt.PreCondition(src => src.SignedAgreement != null);
                opt.MapFrom(src => src.SignedAgreement.DocumentGuid);
            })
            .ForMember(dest => dest.AgreementType, opt => opt.MapFrom(src => src.AgreementVersion.AgreementType));

        CreateMap<EnrolleeNote, EnrolleeNoteViewModel>();
        CreateMap<SiteRegistrationNote, SiteRegistrationNoteViewModel>();

        // DTOs
        CreateMap<Enrollee, AgreementEngineDto>()
            .ForMember(dest => dest.CareSettingCodes, opt => opt.MapFrom(src => src.EnrolleeCareSettings.Select(ecs => ecs.CareSettingCode)));
        CreateMap<Certification, CertificationDto>();
    }
}

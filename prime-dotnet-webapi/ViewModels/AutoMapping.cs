using System.Linq;
using AutoMapper;

using Prime.Models;
using Prime.ViewModels;

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
            .ForMember(dest => dest.HasNewestAgreement, opt => opt.MapFrom(src => newestAgreementIds.Any(n => n == src.CurrentAgreementId)));

        CreateMap<Enrollee, EnrolleeViewModel>()
            .ForMember(dest => dest.CurrentStatusCode, opt => opt.MapFrom(src => src.CurrentStatus.StatusCode))
            .ForMember(dest => dest.AdjudicatorIdir, opt => opt.MapFrom(src => src.Adjudicator.IDIR))
            .ForMember(dest => dest.IsRegulatedUser, opt => opt.MapFrom(src => src.IsRegulatedUser()));

        CreateMap<Agreement, AgreementViewModel>()
            .ForMember(dest => dest.SignedAgreementDocumentGuid, opt =>
            {
                opt.PreCondition(src => src.SignedAgreement != null);
                opt.MapFrom(src => src.SignedAgreement.DocumentGuid);
            })
            .ForMember(dest => dest.AgreementType, opt => opt.MapFrom(src => src.AgreementVersion.AgreementType));

        CreateMap<EnrolleeNote, EnrolleeNoteViewModel>();
        CreateMap<SiteRegistrationNote, SiteRegistrationNoteViewModel>();
    }
}

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
        CreateMap<Organization, OrganizationListViewModel>()
            .ForMember(dest => dest.SignedAgreementDocumentCount, opt => opt.MapFrom(src => src.Agreements.Count(a => a.SignedAgreement != null)));
        CreateMap<Site, SiteListViewModel>()
            .ForMember(dest => dest.AdjudicatorIdir, opt => opt.MapFrom(src => src.Adjudicator.IDIR))
            .ForMember(dest => dest.RemoteUserCount, opt => opt.MapFrom(src => src.RemoteUsers.Count));

        CreateMap<EnrolleeCreateModel, Enrollee>();

        IQueryable<int> newestAgreementIds = null;
        CreateMap<Enrollee, EnrolleeListViewModel>()
            .ForMember(dest => dest.CurrentStatusCode, opt => opt.MapFrom(src => src.CurrentStatus.StatusCode))
            .ForMember(dest => dest.AdjudicatorIdir, opt => opt.MapFrom(src => src.Adjudicator.IDIR))
            .ForMember(dest => dest.HasNewestAgreement, opt => opt.MapFrom(src => newestAgreementIds.Any(n => n == src.CurrentAgreementId)));

        CreateMap<Site, EnrolleeRemoteAccessSiteViewModel>()
           .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.Organization.Name));
    }
}

using System.Linq;
using AutoMapper;

using Prime.Models;
using Prime.Models.DbViews;
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
            .ForMember(dest => dest.SignedAgreementDocumentCount, opt => opt.MapFrom(src => src.SignedAgreementDocuments.Count));
        CreateMap<Site, SiteListViewModel>()
            .ForMember(dest => dest.AdjudicatorIdir, opt => opt.MapFrom(src => src.Adjudicator.IDIR))
            .ForMember(dest => dest.RemoteUserCount, opt => opt.MapFrom(src => src.RemoteUsers.Count));

        CreateMap<EnrolleeCreateModel, Enrollee>();

        CreateMap<Enrollee, EnrolleeViewModel>();

        IQueryable<NewestAgreement> newestAgreements = null;
        CreateMap<Enrollee, EnrolleeListViewModel>()
            .ForMember(dest => dest.CurrentStatusCode, opt => opt.MapFrom(src => src.CurrentStatus.StatusCode))
            .ForMember(dest => dest.AdjudicatorIdir, opt => opt.MapFrom(src => src.Adjudicator.IDIR))
            .ForMember(dest => dest.HasNewestAgreement, opt => opt.MapFrom(src => newestAgreements.Any(n => n.Id == src.CurrentAgreementId)));

        CreateMap<Site, RemoteAccessSiteViewModel>();
        //    .ForMember(dest => dest.OrganizationName, opt => opt.MapFrom(src => src.Organization.Name));

        CreateMap<RemoteAccessSite, RemoteAccessSiteViewModel>()
            .ForMember(dest => dest.SiteId, opt => opt.MapFrom(src => src.Site.Id))
            .ForMember(dest => dest.DoingBusinessAs, opt => opt.MapFrom(src => src.Site.DoingBusinessAs))
            .ForMember(dest => dest.PhysicalAddress, opt => opt.MapFrom(src => src.Site.PhysicalAddress))
            .ForMember(dest => dest.RemoteUsers, opt => opt.MapFrom(src => src.Site.RemoteUsers))
            .ForMember(dest => dest.SiteVendors, opt => opt.MapFrom(src => src.Site.SiteVendors));
    }
}

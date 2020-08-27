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
            .ForMember(vm => vm.SignedAgreementDocumentCount, dest => dest.MapFrom(src => src.SignedAgreementDocuments.Count));
        CreateMap<Site, SiteListViewModel>();

        IQueryable<NewestAgreement> newestAgreements = null;
        CreateMap<Enrollee, EnrolleeListViewModel>()
            .ForMember(dest => dest.CurrentStatusCode, dest => dest.MapFrom(src => src.CurrentStatus.StatusCode))
            .ForMember(dest => dest.AdjudicatorIdir, dest => dest.MapFrom(src => src.Adjudicator.IDIR))
            .ForMember(dest => dest.HasNewestAgreement, dest => dest.MapFrom(src => newestAgreements.Any(n => n.Id == src.CurrentAgreementId)));
    }
}

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
            .ForMember(vm => vm.SignedAgreementDocumentCount, opt => opt.MapFrom(src => src.SignedAgreementDocuments.Count));
        CreateMap<Site, SiteListViewModel>();
    }
}

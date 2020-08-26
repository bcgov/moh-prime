using System;
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
        // CreateMap<Organization, OrganizationViewModel>();
        // CreateMap<Site, SiteViewModel>();
        CreateMap<Enrollee, EnrolleeListViewModel>()
            .ForMember(dest => dest.CurrentStatusCode, dest => dest.MapFrom(src => src.CurrentStatus.StatusCode))
            .ForMember(dest => dest.AdjudicatorIdir, dest => dest.MapFrom(src => src.Adjudicator.IDIR));
    }
}

using System.Linq;
using AutoMapper;

using Prime.Models;
using Prime.Models.HealthAuthorities;
using Prime.ViewModels.Parties;
using Prime.ViewModels.HealthAuthorities;
using Prime.ViewModels.HealthAuthoritySites;
using System;

namespace Prime.ViewModels.Profiles
{
    public class HealthAuthorityMappingProfile : Profile
    {
        public HealthAuthorityMappingProfile()
        {
            IQueryable<int> underReviewIds = null;
            CreateMap<HealthAuthorityOrganization, HealthAuthorityListViewModel>()
                .ForMember(dest => dest.HasUnderReviewUsers, opt => opt.MapFrom(src => underReviewIds.Contains(src.Id)));
            CreateMap<HealthAuthorityOrganization, HealthAuthorityViewModel>();

            CreateMap<HealthAuthorityVendor, HealthAuthorityVendorViewModel>();
            CreateMap<HealthAuthorityCareType, HealthAuthorityCareTypeViewModel>();

            CreateMap<PrivacyOffice, PrivacyOfficeViewModel>()
                .ForMember(dest => dest.PrivacyOfficer, opt => opt.MapFrom(src => src.HealthAuthorityOrganization.PrivacyOfficers.Select(x => x.Contact).SingleOrDefault()))
                .ReverseMap();

            CreateMap<Contact, PrivacyOfficerViewModel>();
            CreateMap<Contact, HealthAuthorityContactViewModel>();

            CreateMap<HealthAuthorityContact, HealthAuthorityContactViewModel>()
                // See https://automapperdocs.readthedocs.io/en/latest/Mapping-inheritance.html
                .IncludeAllDerived()
                .IncludeMembers(src => src.Contact)
                .ForMember(dest => dest.ContactId, opt => opt.MapFrom(src => src.Contact.Id));
            // TODO:
            CreateMap<HealthAuthorityTechnicalSupport, TechnicalSupportContactViewModel>()
                .AfterMap<MapVendorsWorkedWithAction>();

            CreateMap<AuthorizedUser, AuthorizedUserViewModel>()
                .IncludeMembers(src => src.Party);
            CreateMap<Party, AuthorizedUserViewModel>();
        }
    }


    public class MapVendorsWorkedWithAction : IMappingAction<HealthAuthorityTechnicalSupport, TechnicalSupportContactViewModel>
    {
        private readonly ApiDbContext _context;

        public MapVendorsWorkedWithAction(ApiDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Process(HealthAuthorityTechnicalSupport source, TechnicalSupportContactViewModel destination, ResolutionContext context)
        {
            System.Diagnostics.Debug.WriteLine($"Inside {nameof(Process)}");
        }
    }
}

using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using Prime.Models;
using Prime.Models.HealthAuthorities;
using Prime.DTOs.AgreementEngine;
using Prime.ViewModels;
using Prime.ViewModels.Emails;
using Prime.ViewModels.Parties;
using Prime.ViewModels.HealthAuthorities;
using Prime.ViewModels.HealthAuthoritySites;
using Prime.ViewModels.Plr;
using Prime.Models.Plr;
using Prime;

/**
 * Automapper Documentation
 * @see https://docs.automapper.org/en/stable
 */
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        int? careSettingCode = null;
        CreateMap<Organization, OrganizationListViewModel>()
            .ForMember(dest => dest.HasAcceptedAgreement, opt => opt.MapFrom(src => src.Agreements.Any(a => a.AcceptedDate.HasValue)))
            .ForMember(dest => dest.HasSubmittedSite, opt => opt.MapFrom(src => src.Sites.Any(s => s.SubmittedDate.HasValue)))
            .ForMember(dest => dest.Sites, opt => opt.MapFrom(src => src.Sites.Where(s => careSettingCode == null || s.CareSettingCode == careSettingCode)));
        CreateMap<Site, SiteListViewModel>()
            .ForMember(dest => dest.AdjudicatorIdir, opt => opt.MapFrom(src => src.Adjudicator.IDIR))
            .ForMember(dest => dest.RemoteUserCount, opt => opt.MapFrom(src => src.RemoteUsers.Count));
        CreateMap<BusinessLicence, BusinessLicence>();


        CreateMap<SiteRegistrationNote, SiteRegistrationNoteViewModel>();

        // DTOs


        CreateMap<GisEnrolment, GisViewModel>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Party.UserId))
            .ForMember(dest => dest.HPDID, opt => opt.MapFrom(src => src.Party.HPDID))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Party.FirstName))
            .ForMember(dest => dest.GivenNames, opt => opt.MapFrom(src => src.Party.GivenNames))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Party.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Party.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Party.Phone));





        // Don't copy over primary keys
        CreateMap<PlrProvider, PlrProvider>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Ipc, opt => opt.Ignore());

        IQueryable<PlrRoleType> plrRoleTypes = null;
        IQueryable<PlrStatusReason> plrStatusReasons = null;
        CreateMap<PlrProvider, PlrViewModel>()
            .ForMember(dest => dest.ProviderRoleType, opt => opt.MapFrom(src => plrRoleTypes.Where(r => src.ProviderRoleType == r.Code).FirstOrDefault().Name))
            .ForMember(dest => dest.StatusReasonCode, opt => opt.MapFrom(src => plrStatusReasons.Where(s => src.StatusReasonCode == s.Code).FirstOrDefault().Name))
            .ForMember(dest => dest.ExpertiseCode, opt => opt.MapFrom(src => src.Expertise))
            .ForMember(dest => dest.Expertise, opt => opt.Ignore());
    }
}

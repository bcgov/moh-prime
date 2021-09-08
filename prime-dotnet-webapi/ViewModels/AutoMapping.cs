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


        // DTOs


        CreateMap<GisEnrolment, GisViewModel>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Party.UserId))
            .ForMember(dest => dest.HPDID, opt => opt.MapFrom(src => src.Party.HPDID))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Party.FirstName))
            .ForMember(dest => dest.GivenNames, opt => opt.MapFrom(src => src.Party.GivenNames))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Party.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Party.Email))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Party.Phone));






    }
}

using System.Linq;
using AutoMapper;

using Prime.Models;
using Prime.Models.Plr;
using Prime.ViewModels.Plr;

namespace Prime.ViewModels.Profiles
{
    public class PlrMappingProfile : Profile
    {
        public PlrMappingProfile()
        {
            IQueryable<PlrRoleType> plrRoleTypes = null;
            IQueryable<PlrStatusReason> plrStatusReasons = null;
            CreateMap<PlrProvider, PlrViewModel>()
                .ForMember(dest => dest.ProviderRoleType, opt => opt.MapFrom(src => plrRoleTypes.Where(r => src.ProviderRoleType == r.Code).FirstOrDefault().Name))
                .ForMember(dest => dest.StatusReasonCode, opt => opt.MapFrom(src => plrStatusReasons.Where(s => src.StatusReasonCode == s.Code).FirstOrDefault().Name))
                .ForMember(dest => dest.ExpertiseCode, opt => opt.MapFrom(src => src.Expertise))
                .ForMember(dest => dest.Expertise, opt => opt.Ignore());

            // Don't copy over primary keys
            CreateMap<PlrProvider, PlrProvider>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Ipc, opt => opt.Ignore());
        }
    }
}

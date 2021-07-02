using AutoMapper;
using Prime.Models.HealthAuthorities;
using Prime.ViewModels.HealthAuthoritySites;

public class HealthAuthoritySiteMappingProfile : Profile
{
    public HealthAuthoritySiteMappingProfile()
    {
        CreateMap<HealthAuthoritySite, HealthAuthoritySiteViewModel>()
            .ForMember(dest => dest.VendorCode, opt => opt.MapFrom(src => src.Vendor.VendorCode))
            // .ForMember(dest => dest., opt => opt.MapFrom(src => src.Vendor.VendorCode));

        CreateMap<HealthAuthoritySiteInfoViewModel, HealthAuthoritySite>();
        // CreateMap<HealthAuthoritySiteVendorViewModel, HealthAuthoritySite>();

        // .ForMember(dest => dest.GivenNames, opt => opt.MapFrom(src => (src.MiddleName == null) ? src.FirstName : $"{src.FirstName} {src.MiddleName}"));
    }
}

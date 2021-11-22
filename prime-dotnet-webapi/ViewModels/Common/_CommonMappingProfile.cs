using AutoMapper;

using Prime.Models;

namespace Prime.ViewModels.Profiles
{
    public class CommonMappingProfile : Profile
    {
        public CommonMappingProfile()
        {
            CreateMap<Contact, ContactViewModel>()
                .ReverseMap();

            CreateMap<Address, AddressViewModel>();

            CreateMap<AddressViewModel, PhysicalAddress>();
            CreateMap<AddressViewModel, MailingAddress>();
            CreateMap<AddressViewModel, VerifiedAddress>();

            CreateMap<PhysicalAddress, AddressViewModel>();
            CreateMap<MailingAddress, AddressViewModel>();
            CreateMap<VerifiedAddress, AddressViewModel>();
        }
    }
}

using AutoMapper;

using System.Collections.Generic;
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
            CreateMap<AddressViewModel, AdditionalAddress>();

            CreateMap<PhysicalAddress, AddressViewModel>();
            CreateMap<MailingAddress, AddressViewModel>();
            CreateMap<VerifiedAddress, AddressViewModel>();
            CreateMap<AdditionalAddress, AddressViewModel>();
        }
    }
}

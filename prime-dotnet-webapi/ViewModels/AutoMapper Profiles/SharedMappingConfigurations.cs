using System.Linq;
using AutoMapper;

using Prime.Models;
using Prime.ViewModels;

namespace Prime.Infrastructure.AutoMapperProfiles
{
    public class SharedMappingConfigurations : Profile
    {
        public SharedMappingConfigurations()
        {
            CreateMap<Agreement, AgreementViewModel>()
                .ForMember(dest => dest.SignedAgreementDocumentGuid, opt =>
                {
                    opt.PreCondition(src => src.SignedAgreement != null);
                    opt.MapFrom(src => src.SignedAgreement.DocumentGuid);
                })
                .ForMember(dest => dest.AgreementType, opt => opt.MapFrom(src => src.AgreementVersion.AgreementType));
        }
    }
}

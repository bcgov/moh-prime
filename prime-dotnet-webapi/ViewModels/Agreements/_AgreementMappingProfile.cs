using AutoMapper;

using Prime.Models;

namespace Prime.ViewModels.Profiles
{
    public class AgreementMappingProfile : Profile
    {
        public AgreementMappingProfile()
        {
            CreateMap<Agreement, AgreementViewModel>()
                .ForMember(dest => dest.SignedAgreementDocumentGuid, opt =>
                {
                    opt.PreCondition(src => src.SignedAgreement != null);
                    opt.MapFrom(src => src.SignedAgreement.DocumentGuid);
                })
                .ForMember(dest => dest.AgreementType, opt => opt.MapFrom(src => src.AgreementVersion.AgreementType));

            CreateMap<AgreementVersion, AgreementVersionViewModel>();
            CreateMap<AgreementVersion, AgreementVersionListViewModel>();
        }
    }
}

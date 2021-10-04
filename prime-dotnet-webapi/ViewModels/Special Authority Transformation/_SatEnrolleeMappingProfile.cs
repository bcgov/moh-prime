using AutoMapper;
using Prime.Models;
using Prime.ViewModels.SpecialAuthorityTransformation;

namespace Prime.ViewModels.Profiles
{
    public class SatEnrolleeMappingProfile : Profile
    {
        public SatEnrolleeMappingProfile()
        {
            CreateMap<SatEnrolleeDemographicViewModel, Party>();
            CreateMap<SatEnrolleeCertificationViewModel, PartyCertification>();
        }
    }
}
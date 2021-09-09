using AutoMapper;

using Prime.Models;
using Prime.ViewModels.PaperEnrollees;

namespace Prime.ViewModels.Profiles
{
    public class PaperEnrolleeMappingProfile : Profile
    {
        public PaperEnrolleeMappingProfile()
        {
            CreateMap<PaperEnrolleeDemographicViewModel, Enrollee>();
            CreateMap<PaperEnrolleeCertificationViewModel, Certification>();
            CreateMap<PaperEnrolleeSelfDeclarationViewModel, SelfDeclaration>();
            CreateMap<PaperEnrolleeOboSiteViewModel, OboSite>();
        }
    }
}

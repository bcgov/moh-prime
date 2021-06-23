using System.Linq;
using AutoMapper;
using Prime.Models;
using Prime.ViewModels.PaperEnrollees;

public class PaperEnrolleesMappingProfile : Profile
{
    public PaperEnrolleesMappingProfile()
    {
        CreateMap<PaperEnrolleeDemographicViewModel, Enrollee>()
            .ForMember(dest => dest.GivenNames, opt => opt.MapFrom(src => src.MiddleName == null ? src.FirstName : $"{src.FirstName} {src.MiddleName}"));

        CreateMap<PaperEnrolleeCertificationViewModel, Certification>();
        CreateMap<PaperEnrolleeOboSiteViewModel, OboSite>();
    }
}

using System.Linq;
using AutoMapper;
using Prime.Models;
using Prime.ViewModels.PaperEnrollees;

public class PaperEnrolleesMappingProfile : Profile
{
    public PaperEnrolleesMappingProfile()
    {
        CreateMap<PaperEnrolleeDemographicViewModel, Enrollee>()
            .ForMember(dest => dest.GivenNames, opt => opt.MapFrom(src => string.Join(" ", src.FirstName, src.MiddleName)));

        CreateMap<PaperEnrolleeCertificationViewModel, Certification>();
    }
}

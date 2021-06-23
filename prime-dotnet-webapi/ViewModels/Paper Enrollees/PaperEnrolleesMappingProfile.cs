using System.Linq;
using AutoMapper;
using Prime.Models;
using Prime.ViewModels.PaperEnrollees;

public class PaperEnrolleesMappingProfile : Profile
{
    public PaperEnrolleesMappingProfile()
    {
        CreateMap<PaperEnrolleeDemographicViewModel, Enrollee>();
        CreateMap<PaperEnrolleeCertificationViewModel, Certification>();
    }
}

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Prime.Models;
using Prime.Models.Api;
using Prime.ViewModels;
using Prime.ViewModels.PaperEnrollees;

namespace Prime.Services
{
    public interface IEnrolleePaperSubmissionService
    {
        Task<Enrollee> CreateEnrolleeAsync(PaperEnrolleeDemographicViewModel enrollee);
    }
}

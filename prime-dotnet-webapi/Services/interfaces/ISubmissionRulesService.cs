using System.Collections.Generic;
using System.Threading.Tasks;

using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface ISubmissionRulesService
    {
        Task<bool> QualifiesForAutomaticAdjudicationAsync(Enrollee enrollee, bool ignoreDOBDiscrepancy = false);

        Task<bool> QualifiesAsMinorUpdateAsync(Enrollee enrollee, EnrolleeUpdateModel profileUpdate, List<int> newestAgreementVersionIds);
    }
}

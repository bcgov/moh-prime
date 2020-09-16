using System.Threading.Tasks;

using Prime.Models;
using Prime.ViewModels;

namespace Prime.Services
{
    public interface ISubmissionRulesService
    {
        Task<bool> QualifiesForAutomaticAdjudicationAsync(Enrollee enrollee);

        Task<bool> QualifiesAsMinorUpdateAsync(Enrollee enrollee, EnrolleeUpdateModel profileUpdate);
    }
}

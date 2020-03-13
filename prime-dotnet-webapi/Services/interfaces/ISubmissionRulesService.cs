using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public interface ISubmissionRulesService
    {
        Task<bool> QualifiesForAutomaticAdjudication(Enrollee enrollee);
    }
}

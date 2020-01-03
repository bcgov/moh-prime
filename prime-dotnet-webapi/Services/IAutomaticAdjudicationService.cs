using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public interface IAutomaticAdjudicationService
    {
        Task<bool> QualifiesForAutomaticAdjudication(Enrollee enrollee);
    }
}

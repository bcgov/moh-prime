using Prime.Models;

namespace Prime.Services
{
    public interface IAutomaticAdjudicationService
    {            
        bool QualifiesForAutomaticAdjudication(Enrolment enrolment);
    }
}

using Prime.Models;
using Prime.Services;

namespace PrimeTests.Mocks
{
    public class AutomaticAdjudicationServiceMock : BaseMockService, IAutomaticAdjudicationService
    {
        public AutomaticAdjudicationServiceMock() : base()
        { }

        public override void SeedData()
        {
            // no data to seed, as it is done in the base class
        }
        
        public bool QualifiesForAutomaticAdjudication(Enrolment enrolment)
        {
            // TODO - make this have more logic to help testing
            return false;
        }

    }
}

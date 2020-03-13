using System.Threading.Tasks;

using Prime.Models;
using Prime.Services;

namespace PrimeTests.Mocks
{
    public class SubmissionRulesServiceMock : BaseMockService, ISubmissionRulesService
    {
        public SubmissionRulesServiceMock() : base()
        { }

        public override void SeedData()
        {
            // no data to seed, as it is done in the base class
        }

        public Task<bool> QualifiesForAutomaticAdjudication(Enrollee enrollee)
        {
            // TODO - make this have more logic to help testing
            return Task.FromResult(false);
        }
    }
}

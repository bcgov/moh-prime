using System.Threading.Tasks;

using Prime.Models;
using Prime.Services;
using Prime.ViewModels;

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

        public Task<bool> QualifiesForAutomaticAdjudicationAsync(Enrollee enrollee)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> QualifiesAsMinorUpdateAsync(Enrollee enrollee, EnrolleeUpdateModel profileUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}

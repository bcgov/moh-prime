using System.Threading.Tasks;
using Prime.Models;
using Prime.Services;

namespace PrimeTests.Mocks
{
    public class PrivilegeServiceMock : BaseMockService, IPrivilegeService
    {
        public PrivilegeServiceMock() : base()
        { }

        public Task AssignPrivilegesToEnrolleeAsync(int enrolleeId, Enrollee enrollee)
        {
            throw new System.NotImplementedException();
        }

        public override void SeedData()
        {
            // no data to seed, as it is done in the base class
        }


    }
}

using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;

namespace PrimeTests.UnitTests
{
    public class PrivilegeServiceTests : InMemoryDbTest
    {
        public PrivilegeService CreateService(
            IHttpContextAccessor httpContext = null)
        {
            return new PrivilegeService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>()
            );
        }
    }
}

//     public interface IPrivilegeService
//     {
//         Task AssignPrivilegesToEnrolleeAsync(int enrolleeId, Enrollee enrollee);

//         Task<ICollection<AssignedPrivilege>> GetAssignedPrivilegesForEnrolleeAsync(int enrolleeId);

//         Task<ICollection<Privilege>> GetPrivilegesForEnrolleeAsync(Enrollee enrollee);

//     }

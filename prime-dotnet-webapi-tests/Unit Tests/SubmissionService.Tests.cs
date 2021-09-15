using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;
using FakeItEasy;

using Prime;
using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;
using Microsoft.Extensions.Logging;

namespace PrimeTests.UnitTests
{
    public class SubmissionServiceTests : InMemoryDbTest
    {

    }
}

//     public interface ISubmissionService
//     {
//         Task SubmitApplicationAsync(int enrolleeId, EnrolleeUpdateModel enrolleProfile);

//         Task PerformSubmissionActionAsync(int enrolleeId, SubmissionAction action, bool isAdmin);

//         Task UpdateAlwaysManualAsync(int enrolleeId, bool alwaysManual);
//     }

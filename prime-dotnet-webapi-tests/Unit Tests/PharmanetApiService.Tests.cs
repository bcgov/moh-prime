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
    public class PharmanetApiServiceTests : InMemoryDbTest
    {
        public PharmanetApiService CreateService(
            IHttpContextAccessor httpContext = null)
        {
            return new PharmanetApiService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>()
            );
        }
    }
}

//     public interface IPharmanetApiService
//     {
//         Task<PharmanetCollegeRecord> GetCollegeRecordAsync(Certification certification);
//     }

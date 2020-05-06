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
    public class PharmanetApiServiceTests
    {
        public static PharmanetApiService CreateService(
            ApiDbContext context = null,
            IHttpContextAccessor httpContext = null)
        {
            return new PharmanetApiService(
                context ?? A.Fake<ApiDbContext>(),
                httpContext ?? A.Fake<IHttpContextAccessor>()
            );
        }
    }
}

//     public interface IPharmanetApiService
//     {
//         Task<PharmanetCollegeRecord> GetCollegeRecordAsync(Certification certification);
//     }

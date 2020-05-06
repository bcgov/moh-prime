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
    public class BusinessEventServiceTests
    {
        public static BusinessEventService CreateService(
            ApiDbContext context = null,
            IHttpContextAccessor httpContext = null,
            IAdminService adminService = null)
        {
            return new BusinessEventService(
                context ?? A.Fake<ApiDbContext>(),
                httpContext ?? A.Fake<IHttpContextAccessor>(),
                adminService ?? A.Fake<IAdminService>()
            );
        }
    }
}


//     public interface IBusinessEventService
//     {
//         Task<BusinessEvent> CreateStatusChangeEventAsync(int enrolleeId, string description);
//         Task<BusinessEvent> CreateEmailEventAsync(int enrolleeId, string description);
//         Task<BusinessEvent> CreateNoteEventAsync(int enrolleeId, string description);
//         Task<BusinessEvent> CreateAdminClaimEventAsync(int enrolleeId, string description);
//         Task<BusinessEvent> CreateEnrolleeEventAsync(int enrolleeId, string description);
//     }

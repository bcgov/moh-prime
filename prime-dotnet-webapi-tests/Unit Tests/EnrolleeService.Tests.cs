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
    public class EnrolleeServiceTests : InMemoryDbTest
    {
        public EnrolleeService CreateService(
            IHttpContextAccessor httpContext = null,
            ISubmissionRulesService automaticAdjudicationService = null,
            IEmailService emailService = null,
            IPrivilegeService privilegeService = null,
            IAccessTermService accessTermService = null,
            IEnrolleeProfileVersionService enroleeProfileVersionService = null,
            IBusinessEventService businessEventService = null)
        {
            return new EnrolleeService(
                TestDb,
                httpContext ?? A.Fake<IHttpContextAccessor>(),
                automaticAdjudicationService ?? A.Fake<ISubmissionRulesService>(),
                emailService ?? A.Fake<IEmailService>(),
                privilegeService ?? A.Fake<IPrivilegeService>(),
                accessTermService ?? A.Fake<IAccessTermService>(),
                enroleeProfileVersionService ?? A.Fake<IEnrolleeProfileVersionService>(),
                businessEventService ?? A.Fake<IBusinessEventService>()
            );
        }
    }
}

//     public interface IEnrolleeService
//     {
//         Task<Enrollee> GetEnrolleeForUserIdAsync(Guid userId, bool excludeDecline = false);

//         Task<bool> EnrolleeExistsAsync(int enrolleeId);

//         Task<bool> EnrolleeUserIdExistsAsync(Guid userId);

//         Task<bool> EnrolleeGpidExistsAsync(string gpid);

//         Task<Enrollee> GetEnrolleeAsync(int enrolleeId, bool isAdmin = false);

//         Task<Enrollee> GetEnrolleeNoTrackingAsync(int enrolleeId);

//         Task<IEnumerable<Enrollee>> GetEnrolleesAsync(EnrolleeSearchOptions searchOptions = null);

//         Task<int> CreateEnrolleeAsync(Enrollee enrollee);

//         Task<int> UpdateEnrolleeAsync(int enrolleeId, EnrolleeProfileViewModel enrolleeProfile, bool profileCompleted = false);

//         Task DeleteEnrolleeAsync(int enrolleeId);

//         Task<IEnumerable<EnrolmentStatus>> GetEnrolmentStatusesAsync(int enrolleeId);

//         Task<bool> IsEnrolleeInStatusAsync(int enrolleeId, params StatusType[] statusCodesToCheck);

//         Task<IEnumerable<AdjudicatorNote>> GetEnrolleeAdjudicatorNotesAsync(Enrollee enrollee);

//         Task<AdjudicatorNote> CreateEnrolleeAdjudicatorNoteAsync(int enrolleeId, string note, int adminId);

//         Task<IEnrolleeNote> UpdateEnrolleeNoteAsync(int enrolleeId, IEnrolleeNote newNote);

//         Task<int> GetEnrolleeCountAsync();

//         Task<Enrollee> UpdateEnrolleeAdjudicator(int enrolleeId, Admin admin = null);

//         Task<IEnumerable<BusinessEvent>> GetEnrolleeBusinessEvents(int enrolleeId);

//         Task<IEnumerable<HpdidLookup>> HpdidLookupAsync(IEnumerable<string> hpdids);
//     }

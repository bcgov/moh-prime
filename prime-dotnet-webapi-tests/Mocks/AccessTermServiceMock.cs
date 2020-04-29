using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Prime.Models;
using Prime.Services;
using System.Linq;
using PrimeTests.Utils;

namespace PrimeTests.Mocks
{
    public class AccessTermServiceMock : BaseMockService, IAccessTermService
    {
        private static readonly TimeSpan ACCESS_TERM_EXPIRY = TimeSpan.FromDays(365);
        public AccessTermServiceMock() : base()
        { }

        public override void SeedData()
        { }

        public Task<AccessTerm> GetMostRecentNotAcceptedEnrolleesAccessTermAsync(int enrolleeId)
        {
            return Task.FromResult(
                this.GetHolder<int, AccessTerm>().Values?
                    .Where(at => at.EnrolleeId == enrolleeId)
                    .Where(at => at.AcceptedDate == null)
                    .OrderByDescending(at => at.CreatedDate)
                    .FirstOrDefault()
            );
        }

        public Task<AccessTerm> GetMostRecentAcceptedEnrolleesAccessTermAsync(int enrolleeId)
        {
            return Task.FromResult(
               TestUtils.AccessTermFaker.Generate()
            );
        }

        public Task<AccessTerm> GetEnrolleesAccessTermAsync(int enrolleeId, int accessTermId)
        {
            return Task.FromResult(
                this.GetHolder<int, AccessTerm>().Values?
                    .Where(at => at.EnrolleeId == enrolleeId)
                    .Where(at => at.Id == accessTermId)
                    .Where(at => at.AcceptedDate != null)
                    .FirstOrDefault()
            );
        }

        public Task<IEnumerable<AccessTerm>> GetAcceptedAccessTerms(int enrolleeId, int year)
        {
            return Task.FromResult(
                this.GetHolder<int, AccessTerm>().Values?
                    .Where(at => at.EnrolleeId == enrolleeId)
                    .Where(at => at.AcceptedDate != null)
                    .OrderByDescending(at => at.AcceptedDate)
                    .AsEnumerable()
            );
        }

        public Task CreateEnrolleeAccessTermAsync(Enrollee enrollee)
        {
            throw new NotImplementedException();
        }

        public Task AcceptCurrentAccessTermAsync(Enrollee enrollee)
        {
            // var accessTerm = this.GetHolder<int, AccessTerm>().Values?
            //     .Where(at => at.EnrolleeId == enrollee.Id)
            //     .OrderByDescending(at => at.AcceptedDate)
            //     .First();

            // accessTerm.AcceptedDate = DateTime.Now;
            // // Add an Expiry Date of one year in the future.
            // accessTerm.ExpiryDate = DateTime.Now.Add(ACCESS_TERM_EXPIRY);
            return Task.CompletedTask;
        }

        public Task<bool> AccessTermExistsOnEnrolleeAsync(int accessTermId, int enrolleeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsCurrentByEnrolleeAsync(Enrollee enrollee)
        {
            return Task.FromResult(true);
        }

        public Task ExpireCurrentAccessTermAsync(Enrollee enrollee)
        {
            throw new NotImplementedException();
        }
    }
}

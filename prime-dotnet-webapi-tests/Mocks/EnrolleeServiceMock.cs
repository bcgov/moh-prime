using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;

using Prime.Models;
using Prime.Models.Api;
using Prime.Services;
using Prime.ViewModels;
using PrimeTests.Utils;

namespace PrimeTests.Mocks
{
    public class EnrolleeServiceMock : BaseMockService, IEnrolleeService
    {
        public EnrolleeServiceMock() : base()
        { }

        public override void SeedData()
        {
            //seed the enrollees
            IEnumerable<Enrollee> enrollees = TestUtils.EnrolleeFaker.Generate(DEFAULT_ENROLMENTS_SIZE);
            foreach (var enrollee in enrollees)
            {
                this.CreateEnrolleeAsync(enrollee);
            }
        }

        public Task<Enrollee> GetEnrolleeForUserIdAsync(Guid userId)
        {
            return Task.FromResult(this.GetHolder<int, Enrollee>().Values?.SingleOrDefault(e => e.UserId == userId));
        }

        public Task<int> CreateEnrolleeAsync(Enrollee enrollee)
        {
            int enrolleeId = GetNewId(this.GetHolder<int, Enrollee>().Keys);
            enrollee.Id = enrolleeId;

            this.GetHolder<int, Enrollee>().Add(enrolleeId, enrollee);
            return Task.FromResult(enrolleeId);
        }

        public Task DeleteEnrolleeAsync(int enrolleeId)
        {
            this.GetHolder<int, Enrollee>().Remove(enrolleeId);
            return Task.CompletedTask;
        }

        public Task<bool> EnrolleeExistsAsync(int enrolleeId)
        {
            return Task.FromResult(this.GetHolder<int, Enrollee>().ContainsKey(enrolleeId));
        }

        public Task<bool> EnrolleeUserIdExistsAsync(Guid userId)
        {
            var enrollees = this.GetHolder<int, Enrollee>().Values;
            return Task.FromResult(enrollees.Any(e => e.UserId == userId));
        }

        public Task<Enrollee> GetEnrolleeAsync(int enrolleeId, bool isAdmin = false)
        {
            Enrollee enrollee = null;
            this.GetHolder<int, Enrollee>().TryGetValue(enrolleeId, out enrollee);
            return Task.FromResult(enrollee);
        }

        public Task<IEnumerable<Enrollee>> GetEnrolleesAsync(EnrolleeSearchOptions searchOptions)
        {
            IEnumerable<Enrollee> enrollees = this.GetHolder<int, Enrollee>().Values;
            return Task.FromResult(enrollees);
        }

        public Task<int> UpdateEnrolleeAsync(int enrolleeId, EnrolleeProfileViewModel enrolleeProfile, bool profileCompleted)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EnrolmentStatus>> GetEnrolmentStatusesAsync(int enrolleeId)
        {
            Enrollee enrollee = null;
            if (this.GetHolder<int, Enrollee>().ContainsKey(enrolleeId))
            {
                enrollee = this.GetHolder<int, Enrollee>()[enrolleeId];
            }
            return Task.FromResult(enrollee?.EnrolmentStatuses as IEnumerable<EnrolmentStatus>);
        }

        public Task<bool> IsEnrolleeInStatusAsync(int enrolleeId, params int[] statusCodesToCheck)
        {
            if (this.GetHolder<int, Enrollee>().ContainsKey(enrolleeId))
            {
                Enrollee enrollee = this.GetHolder<int, Enrollee>()[enrolleeId];
                return Task.FromResult(statusCodesToCheck.Contains(enrollee.CurrentStatus.StatusCode));
            }
            return Task.FromResult(false);
        }

        public Task<IEnumerable<AdjudicatorNote>> GetEnrolleeAdjudicatorNotesAsync(Enrollee enrollee)
        {
            // TODO add proper tests, but need test spike. Add adjudicatorNote to fake db.
            IEnumerable<AdjudicatorNote> notes = new[] { new AdjudicatorNote() };
            return Task.FromResult(notes);
        }

        public Task<AdjudicatorNote> CreateEnrolleeAdjudicatorNoteAsync(int enrolleeId, string note, int adminId)
        {
            // TODO add proper tests, but need test spike. Add adjudicatorNote to fake db.
            return Task.FromResult(new AdjudicatorNote());
        }

        public Task<IEnrolleeNote> UpdateEnrolleeNoteAsync(int enrolleeId, IEnrolleeNote newNote)
        {
            // TODO add proper tests, but need test spike
            IEnrolleeNote updatedNote = null;
            if (this.GetHolder<int, Enrollee>().ContainsKey(enrolleeId))
            {
                var enrollee = this.GetHolder<int, Enrollee>()[enrolleeId];

                if (newNote.GetType() == typeof(AccessAgreementNote))
                {
                    enrollee.AccessAgreementNote = (AccessAgreementNote)newNote;
                }
                else
                {
                    throw new ArgumentException("Enrollee note type is not recognized, or not allowed.");
                }

                updatedNote = newNote;
            }

            return Task.FromResult(updatedNote);
        }
        public Task<Enrollee> UpdateEnrolleeAlwaysManualAsync(int enrolleeId, bool alwaysManual)
        {

            return Task.FromResult(new Enrollee());
        }

        public Task<Enrollee> GetEnrolleeNoTrackingAsync(int enrolleeId)
        {
            var enrollee = this.GetHolder<int, Enrollee>()[enrolleeId];
            return Task.FromResult(enrollee);
        }

        public Task<int> GetEnrolleeCountAsync()
        {
            throw new NotImplementedException();
        }


        public Task<Enrollee> UpdateEnrolleeAdjudicator(int enrolleeId, Admin admin = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEnrolleeInStatusAsync(int enrolleeId, params StatusType[] statusCodesToCheck)
        {
            var enrollee = this.GetHolder<int, Enrollee>()[enrolleeId];
            bool inStatus = statusCodesToCheck.Cast<int>().Any(sc => sc == enrollee?.CurrentStatus?.StatusCode);
            return Task.FromResult(inStatus);
        }

        public Task<IEnumerable<BusinessEvent>> GetEnrolleeBusinessEvents(int enrolleeId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetGpidForHpdidAsync(string hpdid)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HpdidLookup>> HpdidLookupAsync(IEnumerable<string> hpdids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EnrolleeGpidExistsAsync(string gpid)
        {
            throw new NotImplementedException();
        }
    }
}

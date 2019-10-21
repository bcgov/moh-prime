using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;

using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;

namespace PrimeTests.Mocks
{
    public class EnrolmentServiceMock : IEnrolmentService
    {
        public const int DEFAULT_ENROLMENTS_SIZE = 5;
        public const int MIN_ENROLMENT_ID = 1;
        public const int MAX_ENROLMENT_ID = 1000000;
        public const int MIN_ENROLLEE_ID = 1;
        public const int MAX_ENROLLEE_ID = 1000000;

        private Dictionary<string, object> _fakeDb;

        private const string ENROLMENT_KEY = "enrolments-key";

        public EnrolmentServiceMock()
        {
            this.InitializeDb();
        }

        public void InitializeDb()
        {
            _fakeDb = new Dictionary<string, object>();
            _fakeDb.Add(ENROLMENT_KEY, new Dictionary<int, Enrolment>());
            //seed the enrolments
            IEnumerable<Enrolment> enrolments = TestUtils.EnrolmentFaker.Generate(DEFAULT_ENROLMENTS_SIZE);
            foreach (var enrolment in enrolments)
            {
                this.CreateEnrolmentAsync(enrolment);
            }
        }

        private Dictionary<int, Enrolment> GetEnrolmentHolder()
        {
            return (Dictionary<int, Enrolment>)_fakeDb[ENROLMENT_KEY];
        }

        public Task<int?> CreateEnrolmentAsync(Enrolment enrolment)
        {
            //add the ids, as this is just a fake implementation
            int? enrolmentId = new Faker().Random.Int(MIN_ENROLMENT_ID, MAX_ENROLMENT_ID);
            int? enrolleeId = new Faker().Random.Int(MIN_ENROLLEE_ID, MAX_ENROLLEE_ID);
            enrolment.Id = enrolmentId;
            enrolment.Enrollee.Id = enrolleeId;
            //add in-progress status to this enrolment
            enrolment.EnrolmentStatuses = TestUtils.EnrolmentStatusFaker.Generate(1);

            this.GetEnrolmentHolder().Add((int)enrolmentId, enrolment);
            return Task.FromResult(enrolmentId);
        }

        public Task DeleteEnrolmentAsync(int enrolmentId)
        {
            this.GetEnrolmentHolder().Remove(enrolmentId);
            return Task.CompletedTask;
        }

        public bool EnrolmentExists(int enrolmentId)
        {
            return this.GetEnrolmentHolder().ContainsKey(enrolmentId);
        }

        public Task<Enrolment> GetEnrolmentAsync(int enrolmentId)
        {
            Enrolment enrolment = null;
            if (this.GetEnrolmentHolder().ContainsKey(enrolmentId))
            {
                enrolment = this.GetEnrolmentHolder()[enrolmentId];
            }
            return Task.FromResult(enrolment);
        }

        public Task<Enrolment> GetEnrolmentForUserIdAsync(Guid userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Enrolment>> GetEnrolmentsAsync()
        {
            IEnumerable<Enrolment> enrolments = TestUtils.EnrolmentFaker.Generate(DEFAULT_ENROLMENTS_SIZE);
            return Task.FromResult((IEnumerable<Enrolment>)this.GetEnrolmentHolder().Values?.ToList());
        }

        public Task<IEnumerable<Enrolment>> GetEnrolmentsForUserIdAsync(Guid userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateEnrolmentAsync(Enrolment enrolment)
        {
            int updated = 0;
            int? enrolmentId = enrolment.Id;
            if (enrolmentId != null)
            {
                var found = this.GetEnrolmentHolder().Remove((int)enrolmentId);
                if (found)
                {
                    updated = 1;
                }
                this.GetEnrolmentHolder().Add((int)enrolmentId, enrolment);
            }
            return Task.FromResult(updated);
        }

        public Task<IEnumerable<Status>> GetAvailableEnrolmentStatuses(int enrolmentId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EnrolmentStatus>> GetEnrolmentStatuses(int enrolmentId)
        {
            throw new NotImplementedException();
        }

        public Task<EnrolmentStatus> CreateEnrolmentStatus(int enrolmentId, Status status)
        {
            throw new NotImplementedException();
        }

        public bool IsStatusChangeAllowed(Status startingStatus, Status endingStatus)
        {
            throw new NotImplementedException();
        }

        public bool IsEnrolmentInStatus(int enrolmentId, short statusCodeToCheck)
        {
            Enrolment enrolment = null;
            if (this.GetEnrolmentHolder().ContainsKey(enrolmentId))
            {
                enrolment = this.GetEnrolmentHolder()[enrolmentId];
                return statusCodeToCheck.Equals(enrolment.CurrentStatus?.StatusCode);
            }
            return false;
        }
    }
}
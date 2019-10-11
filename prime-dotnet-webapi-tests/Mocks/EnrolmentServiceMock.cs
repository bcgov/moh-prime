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
            int? enrolmentId = new Faker().Random.Int(1, 1000000);
            enrolment.Id = enrolmentId;
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
            Enrolment enrolment = this.GetEnrolmentHolder()[enrolmentId];
            return Task.FromResult(enrolment);
        }

        public Task<Enrolment> GetEnrolmentForUserIdAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Enrolment>> GetEnrolmentsAsync()
        {
            IEnumerable<Enrolment> enrolments = TestUtils.EnrolmentFaker.Generate(DEFAULT_ENROLMENTS_SIZE);
            return Task.FromResult((IEnumerable<Enrolment>)this.GetEnrolmentHolder().Values.ToList());
        }

        public Task<IEnumerable<Enrolment>> GetEnrolmentsForUserIdAsync(string userId)
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
    }
}
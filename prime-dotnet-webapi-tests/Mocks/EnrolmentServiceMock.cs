using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prime.Models;
using Prime.Services;

namespace PrimeTests.Mocks
{
    public class EnrolmentServiceMock : IEnrolmentService
    {
        public Task<int?> CreateEnrolmentAsync(Enrolment enrolment)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteEnrolmentAsync(int enrolmentId)
        {
            throw new System.NotImplementedException();
        }

        public bool EnrolmentExists(int enrolmentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Enrolment> GetEnrolmentAsync(int enrolmentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Enrolment> GetEnrolmentForUserIdAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Enrolment>> GetEnrolmentsAsync()
        {
            return Task.FromResult(Enumerable.Empty<Enrolment>());
        }

        public Task<IEnumerable<Enrolment>> GetEnrolmentsForUserIdAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> UpdateEnrolmentAsync(Enrolment enrolment)
        {
            throw new System.NotImplementedException();
        }
    }
}
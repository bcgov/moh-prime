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
    public class EnrolleeServiceMock : BaseMockService, IEnrolleeService
    {
        public EnrolleeServiceMock() : base()
        { }

        public override void SeedData()
        {
            //seed the enrollees
            IEnumerable<Enrollee> enrollees = TestUtils.EnrolleeFaker.Generate(DEFAULT_ENROLLEES_SIZE);
            foreach (var enrollee in enrollees)
            {
                //add the ids, as this is just a fake implementation
                int? enrolleeId = new Faker().Random.Int(MIN_ENROLLEE_ID, MAX_ENROLLEE_ID);
                enrollee.Id = enrolleeId;
                this.GetHolder<int, Enrollee>().Add((int)enrolleeId, enrollee);
            }
        }

        public Task<IEnumerable<Enrollee>> GetEnrolleesAsync()
        {
            return Task.FromResult((IEnumerable<Enrollee>)this.GetHolder<int, Enrollee>().Values?.ToList());
        }

        public Task<IEnumerable<Enrollee>> GetEnrolleesForUserIdAsync(Guid userId)
        {
            return Task.FromResult((IEnumerable<Enrollee>)this.GetHolder<int, Enrollee>().Values?.ToList().Where(e => e.UserId == userId));
        }
    }
}
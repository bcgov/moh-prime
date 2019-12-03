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

        public Task<int?> CreateEnrolleeAsync(Enrollee enrollee)
        {
            //add the ids, as this is just a fake implementation
            int? enrolleeId = new Faker().Random.Int(MIN_ENROLMENT_ID, MAX_ENROLMENT_ID);
            enrollee.Id = enrolleeId;

            this.GetHolder<int, Enrollee>().Add((int)enrolleeId, enrollee);
            return Task.FromResult(enrolleeId);
        }

        public Task DeleteEnrolleeAsync(int enrolleeId)
        {
            this.GetHolder<int, Enrollee>().Remove(enrolleeId);
            return Task.CompletedTask;
        }

        public bool EnrolleeExists(int enrolleeId)
        {
            return this.GetHolder<int, Enrollee>().ContainsKey(enrolleeId);
        }

        public Task<Enrollee> GetEnrolleeAsync(int enrolleeId)
        {
            Enrollee enrollee = null;
            if (this.GetHolder<int, Enrollee>().ContainsKey(enrolleeId))
            {
                enrollee = this.GetHolder<int, Enrollee>()[enrolleeId];
            }
            return Task.FromResult(enrollee);
        }

        public Task<IEnumerable<Enrollee>> GetEnrolleesAsync(EnrolleeSearchOptions searchOptions)
        {
            return Task.FromResult((IEnumerable<Enrollee>)this.GetHolder<int, Enrollee>().Values?.ToList());
        }

        public Task<int> UpdateEnrolleeAsync(Enrollee enrollee)
        {
            int updated = 0;
            int? enrolleeId = enrollee.Id;
            if (enrolleeId != null)
            {
                var found = this.GetHolder<int, Enrollee>().Remove((int)enrolleeId);
                if (found)
                {
                    updated = 1;
                }
                this.GetHolder<int, Enrollee>().Add((int)enrolleeId, enrollee);
            }
            return Task.FromResult(updated);
        }

        public Task<IEnumerable<Status>> GetAvailableEnrolleeStatusesAsync(int enrolleeId)
        {
            ICollection<Status> availableStatuses = new List<Status>();
            Enrollee enrollee = null;
            if (this.GetHolder<int, Enrollee>().ContainsKey(enrolleeId))
            {
                enrollee = this.GetHolder<int, Enrollee>()[enrolleeId];
                var results = _workflowStateMap[enrollee.CurrentStatus?.Status ?? this.GetHolder<short, Status>()[NULL_STATUS_CODE]];
                foreach (var item in results)
                {
                    availableStatuses.Add(item.Status);
                }
            }
            return Task.FromResult(availableStatuses as IEnumerable<Status>);
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

        public Task<EnrolmentStatus> CreateEnrolmentStatusAsync(int enrolleeId, Status status)
        {
            ICollection<Status> availableStatuses = new List<Status>();
            Enrollee enrollee = null;
            EnrolmentStatus createdEnrolmentStatus = null;
            if (this.GetHolder<int, Enrollee>().ContainsKey(enrolleeId))
            {
                enrollee = this.GetHolder<int, Enrollee>()[enrolleeId];
                var currentStatusCode = enrollee.CurrentStatus?.StatusCode;

                if (this.IsStatusChangeAllowed(this.GetHolder<short, Status>()[currentStatusCode ?? NULL_STATUS_CODE], status))
                {
                    foreach (var item in enrollee.EnrolmentStatuses)
                    {
                        item.IsCurrent = false;
                    }
                    createdEnrolmentStatus = new EnrolmentStatus { Enrollee = enrollee, EnrolleeId = (int)enrollee.Id, Status = status, StatusCode = status.Code, StatusDate = DateTime.Now, IsCurrent = true };
                    enrollee.EnrolmentStatuses.Add(createdEnrolmentStatus);
                }
            }

            return Task.FromResult(createdEnrolmentStatus);
        }

        public bool IsStatusChangeAllowed(Status startingStatus, Status endingStatus)
        {
            ICollection<Status> availableStatuses = new List<Status>();
            var results = _workflowStateMap[startingStatus ?? this.GetHolder<short, Status>()[NULL_STATUS_CODE]];
            foreach (var item in results)
            {
                availableStatuses.Add(item.Status);
            }

            return availableStatuses.Contains(endingStatus);
        }

        public Task<bool> IsEnrolleeInStatusAsync(int enrolleeId, short statusCodeToCheck)
        {
            Enrollee enrollee = null;
            if (this.GetHolder<int, Enrollee>().ContainsKey(enrolleeId))
            {
                enrollee = this.GetHolder<int, Enrollee>()[enrolleeId];
                return Task.FromResult(statusCodeToCheck.Equals(enrollee.CurrentStatus?.StatusCode));
            }
            return Task.FromResult(false);
        }

        public Task<IEnumerable<Status>> GetAvailableEnrolmentStatusesAsync(int enrolleeId)
        {
            throw new NotImplementedException();
        }
    }
}

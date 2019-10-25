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
    public class EnrolmentServiceMock : BaseMockService, IEnrolmentService
    {
        public EnrolmentServiceMock() : base()
        { }

        public override void SeedData()
        {
            //seed the enrolments
            IEnumerable<Enrolment> enrolments = TestUtils.EnrolmentFaker.Generate(DEFAULT_ENROLMENTS_SIZE);
            foreach (var enrolment in enrolments)
            {
                this.CreateEnrolmentAsync(enrolment);
            }
        }

        public Task<int?> CreateEnrolmentAsync(Enrolment enrolment)
        {
            //add the ids, as this is just a fake implementation
            int? enrolmentId = new Faker().Random.Int(MIN_ENROLMENT_ID, MAX_ENROLMENT_ID);
            int? enrolleeId = new Faker().Random.Int(MIN_ENROLLEE_ID, MAX_ENROLLEE_ID);
            enrolment.Id = enrolmentId;
            enrolment.Enrollee.Id = enrolleeId;

            this.GetHolder<int, Enrolment>().Add((int)enrolmentId, enrolment);
            this.GetHolder<int, Enrollee>().Add((int)enrolleeId, enrolment.Enrollee);
            return Task.FromResult(enrolmentId);
        }

        public Task DeleteEnrolmentAsync(int enrolmentId)
        {
            this.GetHolder<int, Enrolment>().Remove(enrolmentId);
            return Task.CompletedTask;
        }

        public bool EnrolmentExists(int enrolmentId)
        {
            return this.GetHolder<int, Enrolment>().ContainsKey(enrolmentId);
        }

        public Task<Enrolment> GetEnrolmentAsync(int enrolmentId)
        {
            Enrolment enrolment = null;
            if (this.GetHolder<int, Enrolment>().ContainsKey(enrolmentId))
            {
                enrolment = this.GetHolder<int, Enrolment>()[enrolmentId];
            }
            return Task.FromResult(enrolment);
        }

        public Task<Enrolment> GetEnrolmentForUserIdAsync(Guid userId)
        {
            return Task.FromResult(this.GetHolder<int, Enrolment>().Values?.ToList().SingleOrDefault(e => e.Enrollee.UserId == userId));
        }

        public Task<IEnumerable<Enrolment>> GetEnrolmentsAsync(EnrolmentSearchOptions searchOptions)
        {
            return Task.FromResult((IEnumerable<Enrolment>)this.GetHolder<int, Enrolment>().Values?.ToList());
        }

        public Task<IEnumerable<Enrolment>> GetEnrolmentsForUserIdAsync(Guid userId)
        {
            return Task.FromResult((IEnumerable<Enrolment>)this.GetHolder<int, Enrolment>().Values?.ToList().Where(e => e.Enrollee.UserId == userId));
        }

        public Task<int> UpdateEnrolmentAsync(Enrolment enrolment)
        {
            int updated = 0;
            int? enrolmentId = enrolment.Id;
            if (enrolmentId != null)
            {
                var found = this.GetHolder<int, Enrolment>().Remove((int)enrolmentId);
                if (found)
                {
                    updated = 1;
                }
                this.GetHolder<int, Enrolment>().Add((int)enrolmentId, enrolment);
            }
            return Task.FromResult(updated);
        }

        public Task<IEnumerable<Status>> GetAvailableEnrolmentStatusesAsync(int enrolmentId)
        {
            ICollection<Status> availableStatuses = new List<Status>();
            Enrolment enrolment = null;
            if (this.GetHolder<int, Enrolment>().ContainsKey(enrolmentId))
            {
                enrolment = this.GetHolder<int, Enrolment>()[enrolmentId];
                var results = _workflowStateMap[enrolment.CurrentStatus?.Status ?? this.GetHolder<short, Status>()[NULL_STATUS_CODE]];
                foreach (var item in results)
                {
                    availableStatuses.Add(item.Status);
                }
            }
            return Task.FromResult(availableStatuses as IEnumerable<Status>);
        }

        public Task<IEnumerable<EnrolmentStatus>> GetEnrolmentStatusesAsync(int enrolmentId)
        {
            Enrolment enrolment = null;
            if (this.GetHolder<int, Enrolment>().ContainsKey(enrolmentId))
            {
                enrolment = this.GetHolder<int, Enrolment>()[enrolmentId];
            }
            return Task.FromResult(enrolment?.EnrolmentStatuses as IEnumerable<EnrolmentStatus>);
        }

        public Task<EnrolmentStatus> CreateEnrolmentStatusAsync(int enrolmentId, Status status)
        {
            ICollection<Status> availableStatuses = new List<Status>();
            Enrolment enrolment = null;
            EnrolmentStatus createdEnrolmentStatus = null;
            if (this.GetHolder<int, Enrolment>().ContainsKey(enrolmentId))
            {
                enrolment = this.GetHolder<int, Enrolment>()[enrolmentId];
                var currentStatusCode = enrolment.CurrentStatus?.StatusCode;

                if (this.IsStatusChangeAllowed(this.GetHolder<short, Status>()[currentStatusCode ?? NULL_STATUS_CODE], status))
                {
                    foreach (var item in enrolment.EnrolmentStatuses)
                    {
                        item.IsCurrent = false;
                    }
                    createdEnrolmentStatus = new EnrolmentStatus { Enrolment = enrolment, EnrolmentId = (int)enrolment.Id, Status = status, StatusCode = status.Code, StatusDate = DateTime.Now, IsCurrent = true };
                    enrolment.EnrolmentStatuses.Add(createdEnrolmentStatus);
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

        public Task<bool> IsEnrolmentInStatusAsync(int enrolmentId, short statusCodeToCheck)
        {
            Enrolment enrolment = null;
            if (this.GetHolder<int, Enrolment>().ContainsKey(enrolmentId))
            {
                enrolment = this.GetHolder<int, Enrolment>()[enrolmentId];
                return Task.FromResult(statusCodeToCheck.Equals(enrolment.CurrentStatus?.StatusCode));
            }
            return Task.FromResult(false);
        }
    }
}
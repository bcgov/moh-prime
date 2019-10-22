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

        private static short NULL_STATUS_CODE = -1;

        private static Dictionary<short, Status> _statusMap = new Dictionary<short, Status> {
            { NULL_STATUS_CODE, new Status { Code = NULL_STATUS_CODE, Name = "No Status" } },
            { Status.IN_PROGRESS_CODE, new Status { Code = Status.IN_PROGRESS_CODE, Name = "In Progress" } },
            { Status.SUBMITTED_CODE, new Status { Code = Status.SUBMITTED_CODE, Name = "Submitted" } },
            { Status.APPROVED_CODE, new Status { Code = Status.APPROVED_CODE, Name = "Adjudicated/Approved" } },
            { Status.DECLINED_CODE, new Status { Code = Status.DECLINED_CODE, Name = "Declined" } },
            { Status.ACCEPTED_TOS_CODE, new Status { Code = Status.ACCEPTED_TOS_CODE, Name = "Accepted TOS (Terms of Service)" } },
            { Status.DECLINED_TOS_CODE, new Status { Code = Status.DECLINED_TOS_CODE, Name = "Declined TOS (Terms of Service)" } },
         };

        private class StatusWrapper
        {
            public Status Status { get; set; }
            public bool AdminOnly { get; set; }
        }

        private static Dictionary<Status, StatusWrapper[]> _workflowStateMap = new Dictionary<Status, StatusWrapper[]> {
            // construct the workflow map
            { _statusMap[NULL_STATUS_CODE], new StatusWrapper[] { new StatusWrapper { Status = _statusMap[Status.IN_PROGRESS_CODE], AdminOnly = false } } },
            { _statusMap[Status.IN_PROGRESS_CODE], new StatusWrapper[] { new StatusWrapper { Status = _statusMap[Status.SUBMITTED_CODE], AdminOnly = false } } },
            { _statusMap[Status.SUBMITTED_CODE], new StatusWrapper[] { new StatusWrapper { Status = _statusMap[Status.APPROVED_CODE], AdminOnly = true }, new StatusWrapper { Status = _statusMap[Status.DECLINED_CODE], AdminOnly = true } } },
            { _statusMap[Status.APPROVED_CODE], new StatusWrapper[] { new StatusWrapper { Status = _statusMap[Status.ACCEPTED_TOS_CODE], AdminOnly = false }, new StatusWrapper { Status = _statusMap[Status.DECLINED_TOS_CODE], AdminOnly = false } } },
            { _statusMap[Status.DECLINED_CODE], new StatusWrapper[0] },
            { _statusMap[Status.ACCEPTED_TOS_CODE], new StatusWrapper[0] },
            { _statusMap[Status.DECLINED_TOS_CODE], new StatusWrapper[0] }
        };

        private const string ENROLMENT_KEY = "enrolments-key";

        private const string STATUS_KEY = "statuses-key";

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

            _fakeDb.Add(STATUS_KEY, _statusMap);
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

        public Task<IEnumerable<Enrolment>> GetEnrolmentsAsync(EnrolmentSearchOptions searchOptions)
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

        public Task<IEnumerable<Status>> GetAvailableEnrolmentStatusesAsync(int enrolmentId)
        {
            ICollection<Status> availableStatuses = new List<Status>();
            Enrolment enrolment = null;
            if (this.GetEnrolmentHolder().ContainsKey(enrolmentId))
            {
                enrolment = this.GetEnrolmentHolder()[enrolmentId];
                var results = _workflowStateMap[enrolment.CurrentStatus?.Status ?? _statusMap[NULL_STATUS_CODE]];
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
            if (this.GetEnrolmentHolder().ContainsKey(enrolmentId))
            {
                enrolment = this.GetEnrolmentHolder()[enrolmentId];
            }
            return Task.FromResult(enrolment?.EnrolmentStatuses as IEnumerable<EnrolmentStatus>);
        }

        public Task<EnrolmentStatus> CreateEnrolmentStatusAsync(int enrolmentId, Status status)
        {
            ICollection<Status> availableStatuses = new List<Status>();
            Enrolment enrolment = null;
            EnrolmentStatus createdEnrolmentStatus = null;
            if (this.GetEnrolmentHolder().ContainsKey(enrolmentId))
            {
                enrolment = this.GetEnrolmentHolder()[enrolmentId];
                var currentStatusCode = enrolment.CurrentStatus?.StatusCode;

                if (this.IsStatusChangeAllowed(_statusMap[currentStatusCode ?? NULL_STATUS_CODE], status))
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
            var results = _workflowStateMap[startingStatus ?? _statusMap[NULL_STATUS_CODE]];
            foreach (var item in results)
            {
                availableStatuses.Add(item.Status);
            }

            return availableStatuses.Contains(endingStatus);
        }

        public Task<bool> IsEnrolmentInStatusAsync(int enrolmentId, short statusCodeToCheck)
        {
            Enrolment enrolment = null;
            if (this.GetEnrolmentHolder().ContainsKey(enrolmentId))
            {
                enrolment = this.GetEnrolmentHolder()[enrolmentId];
                return Task.FromResult(statusCodeToCheck.Equals(enrolment.CurrentStatus?.StatusCode));
            }
            return Task.FromResult(false);
        }
    }
}
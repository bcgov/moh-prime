using System.Collections.Generic;
using Prime.Models;

namespace PrimeTests.Mocks
{
    public abstract class BaseMockService
    {
        public const int DEFAULT_ENROLMENTS_SIZE = 5;
        public const int DEFAULT_ENROLLEES_SIZE = 5;
        public const int MIN_ENROLMENT_ID = 1;
        public const int MAX_ENROLMENT_ID = 1000000;
        public const int MIN_ENROLLEE_ID = 1;
        public const int MAX_ENROLLEE_ID = 1000000;

        protected static short NULL_STATUS_CODE = -1;

        private static Dictionary<short, Status> _statusMap = new Dictionary<short, Status> {
            { NULL_STATUS_CODE, new Status { Code = NULL_STATUS_CODE, Name = "No Status" } },
            { Status.IN_PROGRESS_CODE, new Status { Code = Status.IN_PROGRESS_CODE, Name = "In Progress" } },
            { Status.SUBMITTED_CODE, new Status { Code = Status.SUBMITTED_CODE, Name = "Submitted" } },
            { Status.APPROVED_CODE, new Status { Code = Status.APPROVED_CODE, Name = "Adjudicated/Approved" } },
            { Status.DECLINED_CODE, new Status { Code = Status.DECLINED_CODE, Name = "Declined" } },
            { Status.ACCEPTED_TOS_CODE, new Status { Code = Status.ACCEPTED_TOS_CODE, Name = "Accepted TOS (Terms of Service)" } },
            { Status.DECLINED_TOS_CODE, new Status { Code = Status.DECLINED_TOS_CODE, Name = "Declined TOS (Terms of Service)" } },
         };

        protected class StatusWrapper
        {
            public Status Status { get; set; }
            public bool AdminOnly { get; set; }
        }

        protected static Dictionary<Status, StatusWrapper[]> _workflowStateMap = new Dictionary<Status, StatusWrapper[]> {
            // construct the workflow map
            { _statusMap[NULL_STATUS_CODE], new StatusWrapper[] { new StatusWrapper { Status = _statusMap[Status.IN_PROGRESS_CODE], AdminOnly = false } } },
            { _statusMap[Status.IN_PROGRESS_CODE], new StatusWrapper[] { new StatusWrapper { Status = _statusMap[Status.SUBMITTED_CODE], AdminOnly = false } } },
            { _statusMap[Status.SUBMITTED_CODE], new StatusWrapper[] { new StatusWrapper { Status = _statusMap[Status.APPROVED_CODE], AdminOnly = true }, new StatusWrapper { Status = _statusMap[Status.DECLINED_CODE], AdminOnly = true } } },
            { _statusMap[Status.APPROVED_CODE], new StatusWrapper[] { new StatusWrapper { Status = _statusMap[Status.ACCEPTED_TOS_CODE], AdminOnly = false }, new StatusWrapper { Status = _statusMap[Status.DECLINED_TOS_CODE], AdminOnly = false } } },
            { _statusMap[Status.DECLINED_CODE], new StatusWrapper[0] },
            { _statusMap[Status.ACCEPTED_TOS_CODE], new StatusWrapper[0] },
            { _statusMap[Status.DECLINED_TOS_CODE], new StatusWrapper[0] }
        };

        private Dictionary<string, object> _fakeDb;

        private readonly string ENROLMENT_KEY = typeof(Enrolment).FullName;
        private readonly string ENROLLEE_KEY = typeof(Enrollee).FullName;
        private readonly string COLLEGE_KEY = typeof(College).FullName;
        private readonly string JOB_NAME_KEY = typeof(JobName).FullName;
        private readonly string LICENSE_KEY = typeof(License).FullName;
        private readonly string ORGANIZATION_NAME_KEY = typeof(OrganizationName).FullName;
        private readonly string ORGANIZATION_TYPE_KEY = typeof(OrganizationType).FullName;
        private readonly string PRACTICE_KEY = typeof(Practice).FullName;
        private readonly string STATUS_KEY = typeof(Status).FullName;

        public BaseMockService()
        {
            this.InitializeDb();
        }

        public void InitializeDb()
        {
            // create the fake database object
            _fakeDb = new Dictionary<string, object>();

            // create the holder for each fake table type
            _fakeDb.Add(ENROLMENT_KEY, new Dictionary<int, Enrolment>());
            _fakeDb.Add(ENROLLEE_KEY, new Dictionary<int, Enrollee>());

            // seed the lookup tables
            _fakeDb.Add(COLLEGE_KEY, new Dictionary<short, College> {
                { 1, new College { Code = 1, Name = "College of Physicians and Surgeons of BC (CPSBC)", Prefix = "91" } },
                { 2, new College { Code = 2, Name = "College of Pharmacists of BC (CPBC)", Prefix = "P1" } },
                { 3, new College { Code = 3, Name = "College of Registered Nurses of BC (CRNBC)", Prefix = "96" } },
                { 4, new College { Code = 4, Name = "None", Prefix = null } }
            });

            _fakeDb.Add(LICENSE_KEY, new Dictionary<short, License> {
                { 1, new License { Code = 1, Name = "Full - General" } },
                { 2, new License { Code = 2, Name = "Full - Pharmacist" } },
                { 3, new License { Code = 3, Name = "Full - Specialty" } },
                { 4, new License { Code = 4, Name = "Registered Nurse" } },
                { 5, new License { Code = 5, Name = "Temporary Registered Nurse" } }
            });

            _fakeDb.Add(PRACTICE_KEY, new Dictionary<short, Practice> {
                { 1, new Practice { Code = 1, Name = "Remote Practice" } },
                { 2, new Practice { Code = 2, Name = "Reproductive Care" } },
                { 3, new Practice { Code = 3, Name = "Sexually Transmitted Infections (STI)" } },
                { 4, new Practice { Code = 4, Name = "None" } }
            });

            _fakeDb.Add(JOB_NAME_KEY, new Dictionary<short, JobName> {
                { 1, new JobName { Code = 1, Name = "Medical Office Assistant" } },
                { 2, new JobName { Code = 2, Name = "Midwife" } },
                { 3, new JobName { Code = 3, Name = "Nurse (not nurse practitioner)" } },
                { 4, new JobName { Code = 4, Name = "Pharmacy Assistant" } },
                { 5, new JobName { Code = 5, Name = "Pharmacy Technician" } },
                { 6, new JobName { Code = 6, Name = "Registration Clerk" } },
                { 7, new JobName { Code = 7, Name = "Ward Clerk" } },
                { 8, new JobName { Code = 8, Name = "Other" } }
            });

            _fakeDb.Add(ORGANIZATION_NAME_KEY, new Dictionary<short, OrganizationName> {
                { 1, new OrganizationName { Code = 1, Name = "Vancouver Island Health" } },
                { 2, new OrganizationName { Code = 2, Name = "Shoppers Drug Mart" } }
            });

            _fakeDb.Add(ORGANIZATION_TYPE_KEY, new Dictionary<short, OrganizationType> {
                { 1, new OrganizationType { Code = 1, Name = "Health Authority" } },
                { 2, new OrganizationType { Code = 2, Name = "Pharmacy" } }
            });

            _fakeDb.Add(STATUS_KEY, _statusMap);

            this.SeedData();
        }

        public abstract void SeedData();

        protected Dictionary<TKey, T> GetHolder<TKey, T>() where T : class
        {
            string key = typeof(T).FullName;
            return _fakeDb[key] as Dictionary<TKey, T>;
        }
    }
}
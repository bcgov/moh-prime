using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Bogus;

using Prime.Models;
using Prime.Services;
using PrimeTests.Utils;

namespace PrimeTests.Mocks
{
    public class LookupServiceMock : ILookupService
    {
        public const int DEFAULT_ENROLMENTS_SIZE = 5;

        private Dictionary<string, Dictionary<short, object>> _fakeDb;

        private string COLLEGE_KEY = typeof(College).FullName;
        private string JOB_NAME_KEY = typeof(JobName).FullName;
        private string LICENSE_KEY = typeof(License).FullName;
        private string ORGANIZATION_NAME_KEY = typeof(OrganizationName).FullName;
        private string ORGANIZATION_TYPE_KEY = typeof(OrganizationType).FullName;
        private string PRACTICE_KEY = typeof(Practice).FullName;
        private string STATUS_KEY = typeof(Status).FullName;

        public LookupServiceMock()
        {
            this.InitializeDb();
        }

        public void InitializeDb()
        {
            _fakeDb = new Dictionary<string, Dictionary<short, object>>();
            _fakeDb.Add(COLLEGE_KEY, new Dictionary<short, object>());
            _fakeDb.Add(JOB_NAME_KEY, new Dictionary<short, object>());
            _fakeDb.Add(LICENSE_KEY, new Dictionary<short, object>());
            _fakeDb.Add(ORGANIZATION_NAME_KEY, new Dictionary<short, object>());
            _fakeDb.Add(ORGANIZATION_TYPE_KEY, new Dictionary<short, object>());
            _fakeDb.Add(PRACTICE_KEY, new Dictionary<short, object>());
            _fakeDb.Add(STATUS_KEY, new Dictionary<short, object>());

            //seed the lookup data
            this.GetHolder<College>(COLLEGE_KEY).Add(1, new College { Code = 1, Name = "College of Physicians and Surgeons of BC (CPSBC)", Prefix = "91" });
            this.GetHolder<College>(COLLEGE_KEY).Add(2, new College { Code = 2, Name = "College of Pharmacists of BC (CPBC)", Prefix = "P1" });
            this.GetHolder<College>(COLLEGE_KEY).Add(3, new College { Code = 3, Name = "College of Registered Nurses of BC (CRNBC)", Prefix = "96" });
            this.GetHolder<College>(COLLEGE_KEY).Add(4, new College { Code = 4, Name = "None", Prefix = null });

            this.GetHolder<License>(LICENSE_KEY).Add(1, new License { Code = 1, Name = "Full - General" });
            this.GetHolder<License>(LICENSE_KEY).Add(2, new License { Code = 2, Name = "Full - Pharmacist" });
            this.GetHolder<License>(LICENSE_KEY).Add(3, new License { Code = 3, Name = "Full - Specialty" });
            this.GetHolder<License>(LICENSE_KEY).Add(4, new License { Code = 4, Name = "Registered Nurse" });
            this.GetHolder<License>(LICENSE_KEY).Add(5, new License { Code = 5, Name = "Temporary Registered Nurse" });

            this.GetHolder<Practice>(PRACTICE_KEY).Add(1, new Practice { Code = 1, Name = "Remote Practice" });
            this.GetHolder<Practice>(PRACTICE_KEY).Add(2, new Practice { Code = 2, Name = "Reproductive Care" });
            this.GetHolder<Practice>(PRACTICE_KEY).Add(3, new Practice { Code = 3, Name = "Sexually Transmitted Infections (STI)" });
            this.GetHolder<Practice>(PRACTICE_KEY).Add(4, new Practice { Code = 4, Name = "None" });

            this.GetHolder<JobName>(JOB_NAME_KEY).Add(1, new JobName { Code = 1, Name = "Medical Office Assistant" });
            this.GetHolder<JobName>(JOB_NAME_KEY).Add(2, new JobName { Code = 2, Name = "Midwife" });
            this.GetHolder<JobName>(JOB_NAME_KEY).Add(3, new JobName { Code = 3, Name = "Nurse (not nurse practitioner)" });
            this.GetHolder<JobName>(JOB_NAME_KEY).Add(4, new JobName { Code = 4, Name = "Pharmacy Assistant" });
            this.GetHolder<JobName>(JOB_NAME_KEY).Add(5, new JobName { Code = 5, Name = "Pharmacy Technician" });
            this.GetHolder<JobName>(JOB_NAME_KEY).Add(6, new JobName { Code = 6, Name = "Registration Clerk" });
            this.GetHolder<JobName>(JOB_NAME_KEY).Add(7, new JobName { Code = 7, Name = "Ward Clerk" });
            this.GetHolder<JobName>(JOB_NAME_KEY).Add(8, new JobName { Code = 8, Name = "Other" });

            this.GetHolder<OrganizationName>(ORGANIZATION_NAME_KEY).Add(1, new OrganizationName { Code = 1, Name = "Vancouver Island Health" });
            this.GetHolder<OrganizationName>(ORGANIZATION_NAME_KEY).Add(2, new OrganizationName { Code = 2, Name = "Shoppers Drug Mart" });

            this.GetHolder<OrganizationType>(ORGANIZATION_TYPE_KEY).Add(1, new OrganizationType { Code = 1, Name = "Health Authority" });
            this.GetHolder<OrganizationType>(ORGANIZATION_TYPE_KEY).Add(2, new OrganizationType { Code = 2, Name = "Pharmacy" });

            this.GetHolder<Status>(STATUS_KEY).Add(1, new Status { Code = 1, Name = "In Progress" });
            this.GetHolder<Status>(STATUS_KEY).Add(2, new Status { Code = 2, Name = "Submitted" });
            this.GetHolder<Status>(STATUS_KEY).Add(3, new Status { Code = 3, Name = "Approved" });
            this.GetHolder<Status>(STATUS_KEY).Add(4, new Status { Code = 4, Name = "Denied" });
            this.GetHolder<Status>(STATUS_KEY).Add(5, new Status { Code = 5, Name = "Accepted" });
        }

        private Dictionary<short, object> GetHolder<T>(string key) where T : ILookup
        {
            return _fakeDb[key];
        }

        Task<List<T>> ILookupService.GetLookupsAsync<T>(params Expression<Func<T, object>>[] includes)
        {
            var type = typeof(T);
            var holder = this.GetHolder<T>(type.FullName);
            var results = new List<T>();
            foreach (var value in holder.Values.ToList())
            {
                results.Add((T)value);
            }

            return Task.FromResult(results);
        }
    }
}
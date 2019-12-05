using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prime.Services
{
    public interface IHibcApiService
    {
        Task<bool> ValidCollegeLicense(string licenceNumber, string collegeReferenceId);
    }
}

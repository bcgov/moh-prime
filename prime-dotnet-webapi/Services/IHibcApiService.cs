using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prime.Services
{
    public interface IHibcApiService
    {
        Task<string> ValidCollegeLicense(string licenceNumber, string collegeReferenceId);
    }
}

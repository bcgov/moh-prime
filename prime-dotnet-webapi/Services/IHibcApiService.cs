using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prime.Services
{
    public interface IHibcApiService
    {
        Task<string> ValidateCollegeLicense(string licenceNumber, string collegeReferenceId);
    }
}

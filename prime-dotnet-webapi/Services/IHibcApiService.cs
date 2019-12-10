using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public interface IHibcApiService
    {
        Task<PharmanetCollegeRecord> GetCollegeRecord(string licenceNumber, string collegeReferenceId);
    }

}

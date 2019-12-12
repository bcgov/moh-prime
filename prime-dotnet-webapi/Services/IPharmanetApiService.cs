using System.Threading.Tasks;

using Prime.Models;

namespace Prime.Services
{
    public interface IPharmanetApiService
    {
        Task<PharmanetCollegeRecord> GetCollegeRecord(Certification certification);
    }
}

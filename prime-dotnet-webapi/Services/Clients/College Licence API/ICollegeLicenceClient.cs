using System.Threading.Tasks;
using Prime.Models;

namespace Prime.Services.Clients
{
    public interface ICollegeLicenceClient
    {
        Task<PharmanetCollegeRecord> GetCollegeRecordAsync(Certification certification);
    }
}

using System.Threading.Tasks;
using Prime.Models;

namespace Prime.HttpClients
{
    public interface ICollegeLicenceClient
    {
        Task<PharmanetCollegeRecord> GetCollegeRecordAsync(string licencePrefix, string licenceNumber);
    }
}

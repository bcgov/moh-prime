using System.Threading.Tasks;

using Prime.HttpClients.PharmanetCollegeApiDefinitions;

namespace Prime.HttpClients
{
    public interface ICollegeLicenceClient
    {
        Task<PharmanetCollegeRecord> GetCollegeRecordAsync(string licencePrefix, string licenceNumber);
    }
}

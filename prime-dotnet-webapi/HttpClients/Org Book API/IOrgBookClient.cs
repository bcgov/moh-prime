using System.Threading.Tasks;

using Prime.HttpClients.PharmanetCollegeApiDefinitions;

namespace Prime.HttpClients
{
    public interface IOrgBookClient
    {
        Task<string> GetOrgBookRegistrationIdAsync(string orgName);
    }
}

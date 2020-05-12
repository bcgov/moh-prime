using System.Threading.Tasks;

namespace Prime.Services
{
    public interface IOrgBookApiService
    {
        Task GetOrganizationInfo(string businessNumber);
    }
}

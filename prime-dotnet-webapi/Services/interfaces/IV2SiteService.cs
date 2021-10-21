using System.Threading.Tasks;

namespace Prime.Services
{
    public interface IV2SiteService
    {
        Task<bool> PecAssignableAsync(int siteId, string pec);
    }
}

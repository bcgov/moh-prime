using System.Security.Claims;
using System.Threading.Tasks;
using Prime.Models;
using Prime.ViewModels.Parties;

namespace Prime.Services
{
    public interface IGisService
    {
        Task<GisViewModel> GetGisEnrolmentByIdAsync(int gisId);
        Task<int> CreateOrUpdateGisEnrolmentAsync(GisChangeModel changeModel, ClaimsPrincipal user);
        Task<bool> LdapLogin(string username, string password, ClaimsPrincipal user);
    }
}

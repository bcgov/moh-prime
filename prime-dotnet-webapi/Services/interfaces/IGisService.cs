using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Prime.ViewModels;
using Prime.ViewModels.Parties;

namespace Prime.Services
{
    public interface IGisService
    {
        Task<GisViewModel> GetGisEnrolmentAsync(int gisId);
        Task<GisViewModel> GetGisEnrolmentAsync(Guid userId);
        Task<int> CreateOrUpdateGisEnrolmentAsync(GisChangeModel changeModel, ClaimsPrincipal user);
        Task<GisLdapUserViewModel> LdapLogin(string username, string password, ClaimsPrincipal user);
        Task SubmitApplicationAsync(int gisId);
    }
}

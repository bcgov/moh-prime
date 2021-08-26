using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Prime.Models;
using Prime.Models.Api;
using Prime.ViewModels.Parties;

namespace Prime.Services
{
    public interface IGisService
    {
        Task<GisViewModel> GetGisEnrolmentByIdAsync(int gisId);
        Task<GisViewModel> GetGisEnrolmentByUserIdAsync(Guid userId);
        Task<int> CreateOrUpdateGisEnrolmentAsync(GisChangeModel changeModel, ClaimsPrincipal user);
        Task<LoginStatus> LdapLogin(string username, string password, ClaimsPrincipal user);
        Task<int> SubmitApplicationAsync(int gisId);
    }
}

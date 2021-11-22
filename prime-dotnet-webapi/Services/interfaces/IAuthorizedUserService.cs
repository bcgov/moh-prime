using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Prime.Models;
using Prime.ViewModels.HealthAuthoritySites;
using Prime.ViewModels.Parties;

namespace Prime.Services
{
    public interface IAuthorizedUserService
    {
        Task<bool> AuthorizedUserExistsAsync(int authorizedUserId);
        Task<bool> AuthorizedUserExistsForUserIdAsync(Guid userId);
        Task<AuthorizedUserViewModel> GetAuthorizedUserAsync(int authorizedUserId);
        Task<AuthorizedUserViewModel> GetAuthorizedUserForUserIdAsync(Guid userId);
        Task<IEnumerable<HealthAuthoritySiteListViewModel>> GetAuthorizedUserSitesAsync(int authorizedUserId);
        Task<int> CreateOrUpdateAuthorizedUserAsync(AuthorizedUserChangeModel changeModel, ClaimsPrincipal user);
        Task ActivateAuthorizedUser(int authorizedUserId);
        Task ApproveAuthorizedUser(int authorizedUserId);
        Task DeleteAuthorizedUserAsync(int authorizedUserId);
    }
}

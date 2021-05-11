using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Prime.Models;
using Prime.ViewModels.Parties;

namespace Prime.Services
{
    public interface IAuthorizedUserService
    {
        Task<bool> AuthorizedUserExistsAsync(int authorizedUserId);
        Task<bool> AuthorizedUserForUserIdAsync(Guid userId);
        Task<AuthorizedUser> GetAuthorizedUserAsync(int authorizedUserId);
        Task<AuthorizedUser> GetAuthorizedUserForUserIdAsync(Guid userId);
        Task<int> CreateOrUpdateAuthorizedUserAsync(AuthorizedUserChangeModel changeModel, ClaimsPrincipal user);
        Task DeleteAuthorizedUserAsync(int authorizedUserId);
    }
}

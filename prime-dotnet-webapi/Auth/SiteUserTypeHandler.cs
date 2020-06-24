using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace Prime.Auth
{
    public class SiteUserTypeHandler : AuthorizationHandler<UserTypeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserTypeRequirement requirement)
        {
            if (requirement.AllowedTypes.Contains(UserType.Site)
                && context.User.IsInRole(AuthConstants.PRIME_USER_ROLE)
                && context.User.GetAudience() == Audiences.Site)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

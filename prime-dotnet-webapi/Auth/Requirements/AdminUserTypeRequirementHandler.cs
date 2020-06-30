using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace Prime.Auth.Requirements
{
    public class AdminUserTypeRequirementHandler : AuthorizationHandler<UserTypeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserTypeRequirement requirement)
        {
            if (requirement.AllowedTypes.Contains(UserType.Admin)
                && context.User.HasAdminView()
                && context.User.GetAudience() == Audiences.Admin)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

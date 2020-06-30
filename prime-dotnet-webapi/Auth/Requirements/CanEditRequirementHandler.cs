using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace Prime.Auth.Requirements
{
    public class CanEditRequirementHandler : AuthorizationHandler<CanEditRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanEditRequirement requirement)
        {
            // Succeed if the User is *not* a read-only Admin (currently, the only role without edit permissions).
            // Note that full admins will have both the prime_admin and prime_readonly_admin roles.
            if (!context.User.HasAdminView()
            || context.User.IsAdmin())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

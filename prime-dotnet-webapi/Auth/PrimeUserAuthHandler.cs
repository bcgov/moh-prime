using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Prime.Auth.Internal
{
    public class PrimeUserAuthHandler : AuthorizationHandler<PrimeUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PrimeUserRequirement requirement)
        {
            context.ThrowIfNull(nameof(context));

            if (context.User.IsInRole(Roles.PrimeAdministrant)
                || context.User.IsInRole(Roles.PrimeEnrollee))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

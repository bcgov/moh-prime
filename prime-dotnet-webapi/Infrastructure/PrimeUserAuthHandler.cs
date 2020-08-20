using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using Prime.Auth;

namespace Prime.Infrastructure
{
    public class PrimeUserAuthHandler : AuthorizationHandler<PrimeUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PrimeUserRequirement requirement)
        {
            context.ThrowIfNull(nameof(context));

            if (context.User.HasAdminView()
                || context.User.IsInRole(AuthConstants.PRIME_ENROLLEE_ROLE))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

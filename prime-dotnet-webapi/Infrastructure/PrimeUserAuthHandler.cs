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
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "The passed in AuthorizationHandlerContext cannot be null.");
            }

            if (context.User.HasAdminView()
                || context.User.IsInRole(AuthConstants.PRIME_ENROLLEE_ROLE))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

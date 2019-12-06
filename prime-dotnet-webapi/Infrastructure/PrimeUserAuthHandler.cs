using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Prime.Infrastructure
{
    public class PrimeUserAuthHandler : AuthorizationHandler<PrimeUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                  PrimeUserRequirement requirement)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "The passed in AuthorizationHandlerContext cannot be null.");
            }

            if (context.User.IsInRole(PrimeConstants.PRIME_ADMIN_ROLE)
                    || (context.User.IsInRole(PrimeConstants.PRIME_ENROLLEE_ROLE)
                            && context.User.HasAssuranceLevel(3)))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

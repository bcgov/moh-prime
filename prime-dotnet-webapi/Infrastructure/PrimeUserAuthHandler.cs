using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Prime.Infrastructure
{
    public class PrimeUserAuthHandler : AuthorizationHandler<PrimeUserRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                  PrimeUserRequirement requirement)
        {
            if (context.User.IsInRole(PrimeConstants.PRIME_ADMIN_ROLE) 
                    || context.User.IsInRole(PrimeConstants.PRIME_ENROLMENT_ROLE))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace Prime.Auth.Requirements
{
    public class EnrolleeUserTypeRequirementHandler : AuthorizationHandler<UserTypeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserTypeRequirement requirement)
        {
            if (requirement.AllowedTypes.Contains(UserType.Enrollee)
                && context.User.IsInRole(AuthConstants.PRIME_USER_ROLE)
                && context.User.GetAudience() == Audiences.Enrolment)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

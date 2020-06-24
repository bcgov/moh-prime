using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace Prime.Auth
{
    public class AudienceRequirementHandler : AuthorizationHandler<AudienceRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AudienceRequirement requirement)
        {
            if (context.User.GetAudience() == requirement.Audience)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

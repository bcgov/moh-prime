using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace Prime.Auth.Requirements
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

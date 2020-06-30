using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace Prime.Auth.Requirements
{
    public class AuthorizedPartyRequirementHandler : AuthorizationHandler<AuthorizedPartyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizedPartyRequirement requirement)
        {
            if (context.User.GetAuthorizedParty() == requirement.AuthorizedParty)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

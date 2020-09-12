using Microsoft.AspNetCore.Authorization;

namespace Prime.Auth.Requirements
{
    public class AuthorizedPartyRequirement : IAuthorizationRequirement
    {
        public string AuthorizedParty { get; set; }

        public AuthorizedPartyRequirement(string authorizedParty)
        {
            AuthorizedParty = authorizedParty;
        }
    }
}

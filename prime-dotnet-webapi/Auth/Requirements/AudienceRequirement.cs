using Microsoft.AspNetCore.Authorization;

namespace Prime.Auth.Requirements
{
    public class AudienceRequirement : IAuthorizationRequirement
    {
        public string Audience { get; set; }

        public AudienceRequirement(string audience)
        {
            Audience = audience;
        }
    }
}

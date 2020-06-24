using Microsoft.AspNetCore.Authorization;

namespace Prime.Auth
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

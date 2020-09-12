using Microsoft.AspNetCore.Authorization;

namespace Prime.Auth.Requirements
{
    public enum UserType
    {
        Enrollee,
        Admin,
        Site,
    }

    public class UserTypeRequirement : IAuthorizationRequirement
    {
        public UserType[] AllowedTypes { get; set; }

        public UserTypeRequirement(params UserType[] allowedTypes)
        {
            AllowedTypes = allowedTypes;
        }
    }
}

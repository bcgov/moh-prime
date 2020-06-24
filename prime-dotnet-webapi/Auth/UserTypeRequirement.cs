using Microsoft.AspNetCore.Authorization;

namespace Prime.Auth
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

        public UserTypeRequirement(UserType[] allowedTypes)
        {
            AllowedTypes = allowedTypes;
        }
    }
}

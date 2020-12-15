using System.Security.Claims;

using Prime.Auth;
using Prime.Models;

namespace Prime.ViewModels.Labtech
{
    public class LabtechCreateModel
    {
        public string Email { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public Party Create(ClaimsPrincipal user)
        {
            return new Party
            {
                Email = Email,
                Phone = Phone,
                PhoneExtension = PhoneExtension,

                UserId = user.GetPrimeUserId(),
                HPDID = user.FindFirstValue(Claims.PreferredUsername),
                FirstName = user.FindFirstValue(Claims.GivenName),
                LastName = user.FindFirstValue(Claims.FamilyName),
                GivenNames = user.FindFirstValue(Claims.GivenNames),
                DateOfBirth = user.GetDateOfBirth().Value,
                PhysicalAddress = user.GetPhysicalAddress(),
            };
        }
    }
}

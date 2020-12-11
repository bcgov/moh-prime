using System.Security.Claims;

using Prime.Auth;
using Prime.ViewModels.Labtech;

namespace Prime.Models
{
    public class Labtech : Party
    {
        public static Labtech From(LabtechCreateModel model, ClaimsPrincipal user)
        {
            return new Labtech
            {
                Email = model.Email,
                Phone = model.Phone,
                PhoneExtension = model.PhoneExtension,

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

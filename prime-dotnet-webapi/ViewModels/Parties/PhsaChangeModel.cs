using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;

using Prime.Configuration.Auth;
using Prime.Models;

namespace Prime.ViewModels.Parties
{
    public class PhsaChangeModel : IPartyChangeModel
    {
        /// <summary> Required </summary>
        public string Email { get; set; }

        /// <summary> Required </summary>
        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public IEnumerable<PartyType> PartyTypes { get; set; }

        /// <summary>
        /// Updates the given Party with values from this CreateModel and the User. Also sets the relevant types in the Party's PartyEnrolments.
        /// Returns the updated Party for convienience.
        /// </summary>
        /// <param name="party"></param>
        /// <param name="user"></param>
        public Party UpdateParty(Party party, ClaimsPrincipal user)
        {
            user.ThrowIfNull(nameof(user));

            party.Email = Email;
            party.Phone = Phone;
            party.PhoneExtension = PhoneExtension;

            party.UserId = user.GetPrimeUserId();
            party.HPDID = user.FindFirstValue(Claims.PreferredUsername);
            party.FirstName = user.GetFirstName();
            party.LastName = user.GetLastName();
            party.GivenNames = user.FindFirstValue(Claims.GivenNames);
            party.DateOfBirth = user.GetDateOfBirth().Value;

            party.SetPartyTypes(PartyTypes.ToArray());

            return party;
        }

        public bool Validate(IEnumerable<PartyType> validPartyTypes)
        {
            if (!PartyTypes.Any() || PartyTypes.Except(validPartyTypes).Any())
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(Email)
                && !string.IsNullOrWhiteSpace(Phone);
        }
    }
}

using System;
using System.Security.Claims;
using Newtonsoft.Json;

using Prime.Auth;
using Prime.Models;

namespace Prime.ViewModels.Parties
{
    public class GisChangeModel : IPartyChangeModel
    {
        [JsonIgnore]
        public Party Party { get; set; }
        public Guid UserId { get; set; }
        public string HPDID { get; set; }
        public string FirstName { get; set; }
        public string GivenNames { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LdapUsername { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Organization { get; set; }
        public string Role { get; set; }

        /// <summary>
        /// Updates the given Party with values from this CreateModel and the User. Also sets the relevant types
        /// in the Party's PartyEnrolments, and returns the updated Party for convenience.
        /// </summary>
        public GisEnrolment UpdateGisParty(GisEnrolment gisEnrolment, ClaimsPrincipal user)
        {
            user.ThrowIfNull(nameof(user));

            gisEnrolment.LdapUsername = LdapUsername;
            gisEnrolment.Organization = Organization;
            gisEnrolment.Role = Role;

            gisEnrolment.Party = UpdateParty(gisEnrolment.Party, user);

            return gisEnrolment;
        }

        public Party UpdateParty(Party party, ClaimsPrincipal user)
        {
            party.Email = Email;
            party.Phone = Phone;

            party.UserId = UserId;
            party.HPDID = HPDID;
            party.FirstName = FirstName;
            party.LastName = LastName;
            party.GivenNames = GivenNames;
            party.DateOfBirth = DateOfBirth;

            party.SetPartyTypes(PartyType.Gis);

            return party;
        }

        public bool Validate(ClaimsPrincipal user)
        {
            return UserId == user.GetPrimeUserId()
                && HPDID == user.FindFirstValue(Claims.PreferredUsername)
                && FirstName == user.FindFirstValue(Claims.GivenName)
                && LastName == user.FindFirstValue(Claims.FamilyName)
                && GivenNames == user.FindFirstValue(Claims.GivenNames)
                && DateOfBirth == user.GetDateOfBirth();
        }
    }
}

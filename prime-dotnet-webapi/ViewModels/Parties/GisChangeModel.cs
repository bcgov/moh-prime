using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;

using Prime.Auth;
using Prime.Models;
using Newtonsoft.Json;
using System;

namespace Prime.ViewModels.Parties
{
    public class GisChangeModel : IPartyChangeModel
    {
        [JsonIgnore]
        public Party Party { get; set; }
        public string LdapUsername { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Organization { get; set; }
        public string Role { get; set; }

        /// <summary>
        /// Updates the given Party with values from this CreateModel and the User. Also sets the relevant types in the Party's PartyEnrolments.
        /// Returns the updated Party for convienience.
        /// </summary>
        /// <param name="gisEnrolment"></param>
        /// <param name="user"></param>
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

            party.UserId = user.GetPrimeUserId();
            party.HPDID = user.FindFirstValue(Claims.PreferredUsername);
            party.FirstName = user.GetFirstName();
            party.LastName = user.GetLastName();
            party.GivenNames = user.FindFirstValue(Claims.GivenNames);
            party.DateOfBirth = user.GetDateOfBirth().Value;

            // party.UserId = new System.Guid();
            // party.HPDID = "aef58bb2-4b6d-41fd-93b3-ffe315592c5c";
            // party.FirstName = "Anais";
            // party.LastName = "Hebert";
            // party.GivenNames = "Anais";
            // party.DateOfBirth = DateTime.Now;

            party.SetPartyTypes(PartyType.Gis);

            return party;
        }
    }
}

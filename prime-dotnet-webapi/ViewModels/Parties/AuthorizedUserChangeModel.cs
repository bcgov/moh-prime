using System.Security.Claims;
using Prime.Models;

namespace Prime.ViewModels.Parties
{
    public class AuthorizedUserChangeModel : IPartyChangeModel
    {
        public string PreferredFirstName { get; set; }
        public string PreferredMiddleName { get; set; }
        public string PreferredLastName { get; set; }
        public VerifiedAddress VerifiedAddress { get; set; }
        public PhysicalAddress PhysicalAddress { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string SmsPhone { get; set; }
        public string JobRoleTitle { get; set; }
        public string EmploymentIdentifier { get; set; }
        public HealthAuthorityCode HealthAuthorityCode { get; set; }

        /// <summary>
        /// Updates the given Party with values from this CreateModel and the User. Also sets the relevant types
        /// in the Party's PartyEnrolments, and returns the updated Party for convenience.
        /// </summary>
        public AuthorizedUser UpdateAuthorizedUser(AuthorizedUser authorizedUser, ClaimsPrincipal user)
        {
            authorizedUser.EmploymentIdentifier = EmploymentIdentifier;
            authorizedUser.HealthAuthorityCode = HealthAuthorityCode;

            authorizedUser.Party = UpdateParty(authorizedUser.Party, user);

            return authorizedUser;
        }

        public Party UpdateParty(Party party, ClaimsPrincipal user)
        {
            party.PreferredFirstName = PreferredFirstName;
            party.PreferredMiddleName = PreferredMiddleName;
            party.PreferredLastName = PreferredLastName;
            party.Email = Email;
            party.Phone = Phone;
            party.SMSPhone = SmsPhone;
            party.JobRoleTitle = JobRoleTitle;

            party.UserId = user.GetPrimeUserId();
            party.FirstName = user.GetFirstName();
            party.LastName = user.GetLastName();
            party.DateOfBirth = user.GetDateOfBirth().Value;

            if (VerifiedAddress != null)
            {
                if (party.VerifiedAddress == null)
                {
                    party.Addresses.Add(new PartyAddress
                    {
                        Party = party,
                        Address = new VerifiedAddress(),
                    });
                }

                VerifiedAddress.Id = party.VerifiedAddress.Id;
                party.VerifiedAddress?.SetValues(VerifiedAddress);
            }

            if (PhysicalAddress != null)
            {
                if (party.PhysicalAddress == null)
                {
                    party.Addresses.Add(new PartyAddress
                    {
                        Party = party,
                        Address = PhysicalAddress,
                    });
                }
                else
                {
                    PhysicalAddress.Id = party.PhysicalAddress.Id;
                    party.PhysicalAddress.SetValues(PhysicalAddress);
                }
            }

            party.SetPartyTypes(PartyType.AuthorizedUser);

            return party;
        }
    }
}

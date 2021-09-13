using System.Security.Claims;

using Prime.Auth;
using Prime.Models;

namespace Prime.ViewModels.Parties
{
    public class SigningAuthorityChangeModel : IPartyChangeModel
    {
        public string PreferredFirstName { get; set; }
        public string PreferredMiddleName { get; set; }
        public string PreferredLastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string SmsPhone { get; set; }
        public string JobRoleTitle { get; set; }
        public MailingAddress MailingAddress { get; set; }
        public PhysicalAddress PhysicalAddress { get; set; }
        public VerifiedAddress VerifiedAddress { get; set; }

        /// <summary>
        /// Updates the given Party with values from this SigningAuthorityCreateModel and the User.
        /// Also sets SigningAuthority in the Party's PartyEnrolments, and returns the updated Party
        /// for convenience.
        /// </summary>
        public Party UpdateParty(Party party, ClaimsPrincipal user)
        {
            party.PreferredFirstName = PreferredFirstName;
            party.PreferredMiddleName = PreferredMiddleName;
            party.PreferredLastName = PreferredLastName;
            party.Email = Email;
            party.Phone = Phone;
            party.Fax = Fax;
            party.SMSPhone = SmsPhone;
            party.JobRoleTitle = JobRoleTitle;

            party.UserId = user.GetPrimeUserId();
            party.FirstName = user.GetFirstName();
            party.LastName = user.GetLastName();
            party.DateOfBirth = user.GetDateOfBirth().Value;

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

            if (MailingAddress != null)
            {
                if (party.MailingAddress == null)
                {
                    party.Addresses.Add(new PartyAddress
                    {
                        Party = party,
                        Address = MailingAddress,
                    });
                }
                else
                {
                    MailingAddress.Id = party.MailingAddress.Id;
                    party.MailingAddress.SetValues(MailingAddress);
                }
            }

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

            party.SetPartyTypes(PartyType.SigningAuthority);

            return party;
        }
    }
}

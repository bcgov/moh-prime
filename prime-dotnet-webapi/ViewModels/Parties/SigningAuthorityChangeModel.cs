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

        /// <summary>
        /// Updates the given Party with values from this SigningAuthorityCreateModel and the User. Also sets SigningAuthority in the Party's PartyEnrolments.
        /// Returns the updated Party for convienience.
        /// </summary>
        /// <param name="party"></param>
        /// <param name="user"></param>
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

            if (party.MailingAddress == null)
            {
                party.MailingAddress = MailingAddress;
            }
            else
            {
                party.MailingAddress.SetValues(MailingAddress);
            }

            if (party.PhysicalAddress == null)
            {
                party.PhysicalAddress = new PhysicalAddress();
            }

            party.PhysicalAddress.SetValues(user.GetVerifiedAddress());

            party.SetPartyTypes(PartyType.SigningAuthority);

            return party;
        }
    }
}

using System;
using System.Security.Claims;
using FluentValidation;
using Prime.Models;
using Prime.ViewModels.Parties;

namespace Prime.ViewModels.SpecialAuthorityTransformation
{
    public class SatEnrolleeDemographicChangeModel : IPartyChangeModel
    {
        /// <summary>
        /// Identifier from Keycloak instance
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Identifier from BCSC
        /// </summary>
        public string HPDID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GivenNames { get; set; }

        public string PreferredFirstName { get; set; }

        public string PreferredMiddleName { get; set; }

        public string PreferredLastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

        public MailingAddress PreferredAddress { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public Party UpdateParty(Party party, ClaimsPrincipal user)
        {
            // Do not destructively change irrelevant fields of the Party object as some fields may
            // come from submissions other than a SAT enrollment

            party.UserId = UserId;
            party.HPDID = HPDID;
            party.FirstName = FirstName;
            party.LastName = LastName;
            party.GivenNames = GivenNames;
            party.DateOfBirth = DateOfBirth;

            party.PreferredFirstName = PreferredFirstName;
            party.PreferredMiddleName = PreferredMiddleName;
            party.PreferredLastName = PreferredLastName;
            party.Email = Email;
            party.Phone = Phone;

            // Values from ClaimsPrincipal have precedence over the change model
            party.UserId = user.GetPrimeUserId();
            party.FirstName = user.GetFirstName();
            party.LastName = user.GetLastName();
            party.DateOfBirth = user.GetDateOfBirth().Value;

            if (PhysicalAddress != null)
            {
                // Add/Update PhysicalAddress
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

            if (PreferredAddress != null)
            {
                // Add/Update MailingAddress
                if (party.MailingAddress == null)
                {
                    party.Addresses.Add(new PartyAddress
                    {
                        Party = party,
                        Address = PreferredAddress,
                    });
                }
                else
                {
                    PreferredAddress.Id = party.MailingAddress.Id;
                    party.MailingAddress.SetValues(PreferredAddress);
                }
            }

            party.SetPartyTypes(PartyType.SatEnrollee);

            return party;
        }
    }

    public class SatEnrolleeDemographicValidator : AbstractValidator<SatEnrolleeDemographicChangeModel>
    {
        public SatEnrolleeDemographicValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.PhysicalAddress).NotNull();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
        }
    }
}

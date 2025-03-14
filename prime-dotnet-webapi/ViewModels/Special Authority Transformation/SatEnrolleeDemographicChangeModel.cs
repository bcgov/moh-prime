using System;
using System.Security.Claims;
using FluentValidation;
using Prime.Configuration.Auth;
using Prime.Models;
using Prime.ViewModels.Parties;

namespace Prime.ViewModels.SpecialAuthorityTransformation
{
    // TODO: Obsolete?
    public class SatEnrolleeDemographicChangeModel : IPartyChangeModel
    {
        /// <summary>
        /// Identifier from Keycloak instance
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Identifier from BCSC.  Health Practitioner Direct Identifier
        /// </summary>
        public string HPDID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string GivenNames { get; set; }

        public string PreferredFirstName { get; set; }

        public string PreferredMiddleName { get; set; }

        public string PreferredLastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Originating from BCSC
        /// </summary>
        public VerifiedAddress VerifiedAddress { get; set; }

        public PhysicalAddress PhysicalAddress { get; set; }

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

            if (VerifiedAddress != null)
            {
                // Add/Update VerifiedAddress of given Party object
                if (party.VerifiedAddress == null)
                {
                    party.Addresses.Add(new PartyAddress
                    {
                        Party = party,
                        Address = VerifiedAddress,
                    });
                }
                else
                {
                    VerifiedAddress.Id = party.VerifiedAddress.Id;
                    party.VerifiedAddress.SetValues(VerifiedAddress);
                }
            }

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

            party.SetPartyTypes(PartyType.SatEnrollee);

            return party;
        }

        public bool Validate(ClaimsPrincipal user)
        {
            return UserId == user.GetPrimeUserId()
               && HPDID == user.FindFirstValue(Claims.PreferredUsername)
               && FirstName == user.FindFirstValue(Claims.GivenName)
               && LastName == user.FindFirstValue(Claims.FamilyName)
               && GivenNames == user.FindFirstValue(Claims.GivenNames)
               && DateOfBirth == user.GetDateOfBirth()
               && Equals(VerifiedAddress, user.GetVerifiedAddress());
        }
    }

    public class SatEnrolleeDemographicValidator : AbstractValidator<SatEnrolleeDemographicChangeModel>
    {
        public SatEnrolleeDemographicValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.DateOfBirth).NotEmpty();
            RuleFor(x => x.VerifiedAddress).NotNull();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty();
        }
    }
}

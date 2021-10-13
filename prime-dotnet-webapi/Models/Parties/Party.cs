using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("Party")]
    public class Party : BaseAuditable, IUserBoundModel, IAgreeable
    {
        [Key]
        public int Id { get; set; }

        public Guid UserId { get; set; }

        [StringLength(255)]
        public string HPDID { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string GivenNames { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PreferredFirstName { get; set; }

        public string PreferredMiddleName { get; set; }

        public string PreferredLastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string JobRoleTitle { get; set; }

        [JsonIgnore]
        public ICollection<PartyAddress> Addresses { get; set; }

        [NotMapped]
        public PhysicalAddress PhysicalAddress
        {
            get => Addresses?
                .Select(a => a.Address)
                .OfType<PhysicalAddress>()
                .SingleOrDefault();
        }

        [NotMapped]
        public MailingAddress MailingAddress
        {
            get => Addresses?
                .Select(a => a.Address)
                .OfType<MailingAddress>()
                .SingleOrDefault();
        }

        [NotMapped]
        public VerifiedAddress VerifiedAddress
        {
            get => Addresses?
                .Select(a => a.Address)
                .OfType<VerifiedAddress>()
                .SingleOrDefault();
        }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public string Fax { get; set; }

        public string SMSPhone { get; set; }

        [JsonIgnore]
        public ICollection<Agreement> Agreements { get; set; }

        public ICollection<PartyEnrolment> PartyEnrolments { get; set; }
        public ICollection<PartySubmission> PartySubmissions { get; set; }

        public ICollection<PartyCertification> PartyCertifications { get; set; }

        /// <summary>
        /// Adds new PartyEnrolments with the given PartyTypes if not already present.
        /// </summary>
        /// <param name="types"></param>
        public void SetPartyTypes(params PartyType[] types)
        {
            PartyEnrolments ??= new List<PartyEnrolment>();

            foreach (var type in types)
            {
                if (PartyEnrolments.All(x => x.PartyType != type))
                {
                    PartyEnrolments.Add(new PartyEnrolment
                    {
                        PartyType = type
                    });
                }
            }
        }
    }
}

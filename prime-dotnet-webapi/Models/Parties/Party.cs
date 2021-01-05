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

        public PhysicalAddress PhysicalAddress { get; set; }

        public MailingAddress MailingAddress { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string PhoneExtension { get; set; }

        public string Fax { get; set; }

        public string SMSPhone { get; set; }

        [JsonIgnore]
        public ICollection<Agreement> Agreements { get; set; }

        public ICollection<PartyEnrolment> PartyEnrolments { get; set; }

        /// <summary>
        /// Adds new PartyEnrolments with the given PartyTypes if not already present.
        /// </summary>
        /// <param name="types"></param>
        public void SetPartyTypes(params PartyType[] types)
        {
            if (PartyEnrolments == null)
            {
                PartyEnrolments = new List<PartyEnrolment>();
            }

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

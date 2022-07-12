using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    public enum ExpiryReasonType
    {
        AnniversaryRenewalRequired = 1,
        ForcedRenewal = 2,
    };

    [Table("Agreement")]
    public class Agreement : BaseAuditable, IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        public int? EnrolleeId { get; set; }

        [JsonIgnore]
        public Enrollee Enrollee { get; set; }

        public int? OrganizationId { get; set; }

        [JsonIgnore]
        public Organization Organization { get; set; }

        public int? PartyId { get; set; }

        [JsonIgnore]
        public Party Party { get; set; }

        [JsonIgnore]
        public int AgreementVersionId { get; set; }

        [JsonIgnore]
        public AgreementVersion AgreementVersion { get; set; }

        [JsonIgnore]
        public int? LimitsConditionsClauseId { get; set; }

        [JsonIgnore]
        public LimitsConditionsClause LimitsConditionsClause { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? AcceptedDate { get; set; }

        public DateTimeOffset? ExpiryDate { get; set; }

        public ExpiryReasonType? ExpiryReason { get; set; }

        public SignedAgreementDocument SignedAgreement { get; set; }

        [NotMapped]
        public string AgreementContent { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Validate that no more than one Foreign key / Navigation property have been set. Zero could be fine, since it is valid to specify only the keys or only the navigation properties when creating an Agreement.
            // This is a simple validation, more accurate validation is performed by the check constraints in the database.

            var foreignKeyCount = new[] { EnrolleeId, OrganizationId, PartyId }.Count(k => k.HasValue);
            if (foreignKeyCount > 1)
            {
                yield return new ValidationResult("Cannot specify more than one foreign key on an Agreement");
            }

            var navPropertiesCount = new object[] { Enrollee, Organization, Party }.Count(n => n != null);
            if (navPropertiesCount > 1)
            {
                yield return new ValidationResult("Cannot specify more than one navigation property on an Agreement");
            }
        }
    }
}

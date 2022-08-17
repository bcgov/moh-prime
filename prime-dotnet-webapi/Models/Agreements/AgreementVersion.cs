using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [Table("AgreementVersion")]
    public class AgreementVersion : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public AgreementType AgreementType { get; set; }

        public DateTimeOffset EffectiveDate { get; set; }

        [NotMapped]
        /// <summary>
        /// Translate the Agreement Type into terms/words provisioner can understand
        /// </summary>
        public string AccessType
        {
            get
            {
                switch (AgreementType)
                {
                    case AgreementType.CommunityPharmacistTOA:
                        return "Independent User – Pharmacy";
                    case AgreementType.RegulatedUserTOA:
                        return "Independent User - with OBOs";
                    case AgreementType.OboTOA:
                        return "On-behalf-of User";
                    case AgreementType.PharmacyOboTOA:
                        return "On-behalf-of User – Pharmacy";
                    // TODO: TBD
                    // case AgreementType.PharmacyTechnicianTOA:
                    //     break;
                    default:
                        return "N/A";
                }
            }
        }
    }
}

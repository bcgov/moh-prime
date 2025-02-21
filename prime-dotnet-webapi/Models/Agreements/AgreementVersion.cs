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

        /// <summary>
        /// Translate the Agreement Type into terms/words provisioner can understand
        /// </summary>
        [NotMapped]
        public string AccessType
        {
            get
            {
                switch (AgreementType)
                {
                    case AgreementType.CommunityPharmacistTOA:
                        return "Independent User – with OBOs, Pharmacy";
                    case AgreementType.RegulatedUserTOA:
                        return "Independent User - with OBOs";
                    case AgreementType.LicencedPracticalNurseTOA:
                        return "Independent User - without OBOs";
                    case AgreementType.OboTOA:
                        return "On-behalf-of User";
                    case AgreementType.PharmacyOboTOA:
                        return "On-behalf-of User – Pharmacy";
                    case AgreementType.DeviceProviderRUTOA:
                        return "Device Provider Agent - with OBOs";
                    case AgreementType.DeviceProviderOBOTOA:
                        return "On-behalf-of User - Device Provider";
                    case AgreementType.PrescriberOBOTOA:
                        return "On-behalf-of User";
                    case AgreementType.PharmacyTechnicianTOA:
                        return "Independent User - without OBOs, Pharmacy";
                    default:
                        return "N/A";
                }
            }
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.Models
{
    [Table("LicenseDetail")]
    public class LicenseDetail : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public int LicenseCode { get; set; }

        public License License { get; set; }

        public DateTime EffectiveDate { get; set; }

        public string Prefix { get; set; }

        public bool Manual { get; set; }

        public bool Validate { get; set; }

        public bool NamedInImReg { get; set; }

        public bool LicensedToProvideCare { get; set; }

        public PrescriberIdType? PrescriberIdType { get; set; }

        // No longer in used - keeping this column to avoid updating the seeding configration
        public bool AllowRequestRemoteAccess { get; set; }

        public string NonPrescribingPrefix { get; set; }

        public bool Multijurisdictional { get; set; }
    }
}

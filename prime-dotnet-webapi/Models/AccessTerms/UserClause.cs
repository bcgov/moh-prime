using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prime.Configuration.Agreements;

namespace Prime.Models
{
    [Table("UserClause")]
    public abstract class UserClause : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTimeOffset EffectiveDate { get; set; }

        /// <summary>
        /// Returns a list containing the IDs of the newest version of each type of Agreement.
        /// </summary>
        public static IEnumerable<int> NewestAgreementIds()
        {
            return AgreementConfiguration.SeedData
                .GroupBy(a => a.GetType())
                .Select(g => g.OrderByDescending(a => a.EffectiveDate).First())
                .Select(a => a.Id);
        }
    }

    public class CommunityPharmacistAgreement : UserClause
    { }
    public class RegulatedUserAgreement : UserClause
    { }
    public class OboAgreement : UserClause
    { }
}

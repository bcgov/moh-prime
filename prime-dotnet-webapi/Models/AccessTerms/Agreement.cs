using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prime.Configuration.Agreements;

namespace Prime.Models
{
    [Table("Agreement")]
    public abstract class Agreement : BaseAuditable
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTimeOffset EffectiveDate { get; set; }

        /// <summary>
        /// Returns a list containing the IDs of the newest version of each type of Agreement.
        /// TODO: Move to a service with cached newest agreement Ids? DI a singleton data object created at app startup?
        /// </summary>
        public static IEnumerable<int> NewestAgreementIds()
        {
            return AgreementConfiguration.SeedData
                .GroupBy(a => a.GetType())
                .Select(group => group.OrderByDescending(a => a.EffectiveDate).First().Id);
        }
    }

    public class CommunityPharmacistAgreement : Agreement
    { }
    public class RegulatedUserAgreement : Agreement
    { }
    public class OboAgreement : Agreement
    { }
}

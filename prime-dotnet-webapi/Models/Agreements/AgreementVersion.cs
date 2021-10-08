using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prime.Configuration.Database;

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
        /// Returns a list containing the IDs of the newest version of each type of Agreement.
        /// TODO: Move to a service with cached newest agreementversion Ids? DI a singleton data object created at app startup?
        /// </summary>
        public static IEnumerable<int> NewestAgreementVersionIds()
        {
            return new AgreementVersionConfiguration().SeedData
                .GroupBy(a => a.AgreementType)
                .Select(group => group.OrderByDescending(a => a.EffectiveDate).First().Id);
        }
    }
}

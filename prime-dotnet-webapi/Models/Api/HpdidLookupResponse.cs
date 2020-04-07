using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models.Api
{
    [NotMapped]
    public class HpdidLookup
    {
        public string Hpdid { get; set; }
        public string Gpid { get; set; }
        public DateTimeOffset? RenewalDate { get; set; }

        public static HpdidLookup FromEnrollee(Enrollee enrollee)
        {
            return new HpdidLookup
            {
                Hpdid = enrollee.HPDID,
                Gpid = enrollee.GPID,
                RenewalDate = enrollee.ExpiryDate
            };
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;

namespace Prime.Models
{
    public interface IAgreeable
    {
        ICollection<Agreement> Agreements { get; set; }

        DateTimeOffset? NewestAcceptedAgreementDate()
        {
            return Agreements?
                .Select(a => a.AcceptedDate)
                .DefaultIfEmpty()
                .Max();
        }
    }
}

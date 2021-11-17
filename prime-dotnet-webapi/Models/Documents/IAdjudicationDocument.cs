using System;

namespace Prime.Models
{
    public interface IAdjudicationDocument
    {
        int AdjudicatorId { get; set; }

        Admin Adjudicator { get; set; }

        int Id { get; set; }

        Guid DocumentGuid { get; set; }

        string Filename { get; set; }

        DateTimeOffset UploadedDate { get; set; }
    }
}

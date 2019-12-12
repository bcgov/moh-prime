using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Prime.Models
{
    public enum NoteType
    {
        AdjudicatorNote,
        AccessAgreementNote,
        EnrolmentCertificateNote
    };

    public interface IEnrolleeNote
    {
        int? Id { get; set; }

        int EnrolleeId { get; set; }

        Enrollee Enrollee { get; set; }

        string Note { get; set; }

        DateTime NoteDate { get; set; }
    }
}

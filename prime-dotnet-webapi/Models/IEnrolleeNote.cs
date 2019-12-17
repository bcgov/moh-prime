using System;

namespace Prime.Models
{
    public interface IEnrolleeNote
    {
        int? Id { get; set; }

        int EnrolleeId { get; set; }

        Enrollee Enrollee { get; set; }

        string Note { get; set; }

        DateTime NoteDate { get; set; }
    }
}

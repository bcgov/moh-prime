using System;

namespace Prime.Models
{
    public interface IBaseEnrolleeNote
    {
        int EnrolleeId { get; set; }

        Enrollee Enrollee { get; set; }

        string Note { get; set; }

        DateTimeOffset NoteDate { get; set; }
    }
}

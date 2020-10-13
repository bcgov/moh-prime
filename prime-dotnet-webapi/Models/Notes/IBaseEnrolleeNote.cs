using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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

using System;

namespace Prime.ViewModels
{
    public class EnrolleeAbsenceViewModel
    {
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        public DateTime StartTimestamp { get; set; }

        public DateTime EndTimestamp { get; set; }
    }
}

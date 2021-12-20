using System;

namespace Prime.ViewModels.Emails
{
    public class EnrolleeAbsenceNotificationEmailViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }

        public EnrolleeAbsenceNotificationEmailViewModel(string firstName, string lastName, DateTime start, DateTime? end)
        {
            FirstName = firstName;
            LastName = lastName;
            Start = start;
            End = end;
        }
    }
}

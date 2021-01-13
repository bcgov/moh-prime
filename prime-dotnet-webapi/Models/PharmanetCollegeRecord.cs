using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [NotMapped]
    public class PharmanetCollegeRecord
    {
        public string ApplicationUUID { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Status { get; set; }
        public DateTimeOffset EffectiveDate { get; set; }

        public bool MatchesEnrolleeByName(Enrollee enrollee)
        {
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                throw new InvalidOperationException("PharmaNet college record is missing the first or last name, cannot be a valid record.");
            }

            static bool IsMatch(string name1, string name2) => name1.Equals(name2, StringComparison.CurrentCultureIgnoreCase);

            return (IsMatch(FirstName, enrollee.FirstName) && IsMatch(LastName, enrollee.LastName))
                || (IsMatch(FirstName, enrollee.PreferredFirstName) && IsMatch(LastName, enrollee.PreferredLastName));
        }
    }
}

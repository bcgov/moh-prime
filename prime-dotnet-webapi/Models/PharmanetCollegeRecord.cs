using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.Models
{
    [NotMapped]
    public class PharmanetCollegeRecord
    {
        public string applicationUUID { get; set; }
        public string firstName { get; set; }
        public string middleInitial { get; set; }
        public string lastName { get; set; }
        public DateTime dateofBirth { get; set; }
        public string status { get; set; }
        public DateTime effectiveDate { get; set; }

        public bool MatchesEnrolleeByName(Enrollee enrollee)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new InvalidOperationException("PharmaNet college record is missing the first or last name, cannot be a valid record.");
            }

            bool IsMatch(string name1, string name2) => name1.Equals(name2, StringComparison.CurrentCultureIgnoreCase);

            return (IsMatch(firstName, enrollee.FirstName) && IsMatch(lastName, enrollee.LastName))
                || (IsMatch(firstName, enrollee.PreferredFirstName) && IsMatch(lastName, enrollee.PreferredLastName));
        }
    }
}

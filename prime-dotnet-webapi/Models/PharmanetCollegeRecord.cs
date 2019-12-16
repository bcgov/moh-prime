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

        public bool MatchesEnrolleeNames(Enrollee enrollee)
        {
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new InvalidOperationException("PharmaNet college record is missing the first or last name, cannot be a valid record.");
            }

            return (firstName.Equals(enrollee.FirstName, StringComparison.CurrentCultureIgnoreCase) && lastName.Equals(enrollee.LastName, StringComparison.CurrentCultureIgnoreCase))
                || (firstName.Equals(enrollee.PreferredFirstName, StringComparison.CurrentCultureIgnoreCase) && lastName.Equals(enrollee.PreferredLastName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}

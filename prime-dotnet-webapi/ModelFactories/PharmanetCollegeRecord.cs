using FactoryGirlCore;
using system;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prime.ModelFactories
{
    [NotMapped]
    public class PharmanetCollegeRecord
    {
        public string applicationUUID ,
        public string firstName ,
        public string middleInitial ,
        public string lastName ,
        public DateTime dateofBirth ,
        public string status ,
        public DateTime effectiveDate ,

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

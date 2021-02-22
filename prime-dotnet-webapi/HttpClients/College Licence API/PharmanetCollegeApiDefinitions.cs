using System;
using Prime.Models;

namespace Prime.HttpClients.PharmanetCollegeApiDefinitions
{
    public class CollegeRecordRequestParams
    {
        public string ApplicationUUID { get; set; }
        public string ProgramArea { get; set; }
        public string LicenceNumber { get; set; }
        public string CollegeReferenceId { get; set; }

        public CollegeRecordRequestParams(string licencePrefix, string licenceNumber)
        {
            ApplicationUUID = Guid.NewGuid().ToString();
            ProgramArea = "PRIME";
            LicenceNumber = licenceNumber;
            CollegeReferenceId = licencePrefix;
        }
    }

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

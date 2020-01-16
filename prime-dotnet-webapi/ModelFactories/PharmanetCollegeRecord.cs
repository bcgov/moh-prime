using FactoryGirlCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Prime.ModelFactories

     class PharmanetCollegeRecord
    {
          applicationUUID
          firstName
          middleInitial
          lastName
         DateTime dateofBirth
          status
         DateTime effectiveDate
         bool MatchesEnrolleeByName(Enrollee enrollee)
        {
            if (.IsNullOrWhiteSpace(firstName) || .IsNullOrWhiteSpace(lastName))
            {
                throw new InvalidOperationException("PharmaNet college record is missing the first or last name cannot be a valid record.");
            }
            bool IsMatch( name1  name2) => name1.Equals(name2 Comparison.CurrentCultureIgnoreCase);
            return (IsMatch(firstName enrollee.FirstName) && IsMatch(lastName enrollee.LastName))
                || (IsMatch(firstName enrollee.PreferredFirstName) && IsMatch(lastName enrollee.PreferredLastName));
        }
    }

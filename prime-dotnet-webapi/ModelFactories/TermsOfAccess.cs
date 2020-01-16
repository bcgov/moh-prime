using FactoryGirlCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Prime.ModelFactories
{

     class TermsOfAccess : IDefinable
    {
         TermsOfAccess()
        {
            // Create lists so they don't have be instantiated when items need to be added
            TermsOfAccessLicenseClassClauses = new List<TermsOfAccessLicenseClassClause>();
            TermsOfAccessLimitsAndConditionsClauses = new List<TermsOfAccessLimitsAndConditionsClause>();
        }


          Id

          EnrolleeId


         Enrollee Enrollee

          GlobalClauseId

         GlobalClause GlobalClause

          UserClauseId

         UserClause UserClause


        // TODO use the get instead of using the service to populate
         List<LicenseClassClause> LicenseClassClauses


        // TODO use the get instead of using the service to populate
         List<LimitsAndConditionsClause> LimitsAndConditionsClauses


         List<TermsOfAccessLicenseClassClause> TermsOfAccessLicenseClassClauses


         List<TermsOfAccessLimitsAndConditionsClause> TermsOfAccessLimitsAndConditionsClauses


         DateTime EffectiveDate
    }
}

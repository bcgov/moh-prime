using FactoryGirlCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Models.AccessAgreement;
namespace Prime.ModelFactories

     class LimitsAndConditionsClause : IDefinable IAccessClause
    {
         LimitsAndConditionsClause()
        {
            // Create lists so they don't have be instantiated when items need to be added
            TermsOfAccessLimitsAndConditionsClauses = new List<TermsOfAccessLimitsAndConditionsClause>();
        }

          Id

          Clause

         DateTime EffectiveDate

         List<TermsOfAccessLimitsAndConditionsClause> TermsOfAccessLimitsAndConditionsClauses
    }

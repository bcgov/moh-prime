using FactoryGirlCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Prime.Models.AccessAgreement;
namespace Prime.ModelFactories

     class LicenseClassClause : IDefinable IAccessClause
    {
         LicenseClassClause()
        {
            // Create lists so they don't have be instantiated when items need to be added
            TermsOfAccessLicenseClassClauses = new List<TermsOfAccessLicenseClassClause>();
        }

          Id

          Clause

         DateTime EffectiveDate

         List<TermsOfAccessLicenseClassClause> TermsOfAccessLicenseClassClauses
    }

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Prime.Models;
using Prime.Configuration.Database.Resources;

namespace Prime.Configuration.Database
{
    public class SelfDeclarationVersionConfiguration : SeededTable<SelfDeclarationVersion>
    {
        public static readonly DateTime InitialEffectiveDate = new(2020, 2, 1, 8, 0, 0, DateTimeKind.Utc);
        public static readonly DateTime Dec2022EffectiveDate = new(2022, 12, 1, 8, 0, 0, DateTimeKind.Utc);

        public override IEnumerable<SelfDeclarationVersion> SeedData
        {
            get
            {
                return new[] {
                    new SelfDeclarationVersion { Id = 1,  SelfDeclarationTypeCode = (int)SelfDeclarationTypeCode.Conviction, Text = "Are you, or have you ever been, the subject of an order or a conviction under legislation in any jurisdiction for a matter that involved improper access to, collection, use, or disclosure or retention of personal information?", EffectiveDate = InitialEffectiveDate, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new SelfDeclarationVersion { Id = 2,  SelfDeclarationTypeCode = (int)SelfDeclarationTypeCode.RegistrationSuspended, Text = "Are you, or have you ever been, subject to any limits, conditions or prohibitions imposed as a result of disciplinary actions taken by a governing body of a health profession in any jurisdiction, that involved improper access to, collection, use, or disclosure or retention of personal information?", EffectiveDate = InitialEffectiveDate, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new SelfDeclarationVersion { Id = 3,  SelfDeclarationTypeCode = (int)SelfDeclarationTypeCode.DisciplinaryAction, Text = "Have you ever been disciplined or fired by an employer, or had a contract for your services terminated, for a matter that involved improper access to, collection, use, or disclosure or retention of personal information?", EffectiveDate = InitialEffectiveDate, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new SelfDeclarationVersion { Id = 4,  SelfDeclarationTypeCode = (int)SelfDeclarationTypeCode.PharmaNetSuspended, Text = "Have you ever had your access to PharmaNet or any other health information system, whether or not electronic,  an electronic health record system, electronic medical record system, pharmacy or laboratory record system, or any similar health information system, in any jurisdiction, suspended or cancelled for a matter that involved improper access to, collection, use, or disclosure or retention of personal information?", EffectiveDate = InitialEffectiveDate, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },

                    // new self declaration questions
                    new SelfDeclarationVersion { Id = 5,  SelfDeclarationTypeCode = (int)SelfDeclarationTypeCode.Conviction, Text = "Have you ever been the subject of <u>an order</u> or <u>conviction</u> in British Columbia or any other jurisdiction <u>for a matter involving an “unlawful or improper action”</u>?", EffectiveDate = Dec2022EffectiveDate, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new SelfDeclarationVersion { Id = 6,  SelfDeclarationTypeCode = (int)SelfDeclarationTypeCode.RegistrationSuspended, Text = "Are you, or have you ever been, subject to the imposition, whether by order or with consent, of <u>prohibitions, limits or conditions on your practice of a health profession:</u> <ol style='list-style-type: lower-alpha;' class='mb-0'><li>in British Columbia, under the Health Professions Act or the Pharmacy Operations and Drug Scheduling Act, or</li><li>in any other jurisdiction, by a body that regulates a health profession in that jurisdiction</li></ol><u>for a matter involving an “unlawful or improper action”</u>?", EffectiveDate = Dec2022EffectiveDate, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new SelfDeclarationVersion { Id = 7,  SelfDeclarationTypeCode = (int)SelfDeclarationTypeCode.DisciplinaryAction, Text = "Has an employer ever disciplined you, or terminated your employment, for <u>a matter involving an “unlawful or improper action”</u>?  Has a contract for your services ever been terminated <u>for a matter involving an “unlawful or improper action”</u>?", EffectiveDate = Dec2022EffectiveDate, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                    new SelfDeclarationVersion { Id = 8,  SelfDeclarationTypeCode = (int)SelfDeclarationTypeCode.PharmaNetSuspended, Text = "Has your access to <u>PharmaNet</u> or <u>any other health information system</u>, whether or not electronic and whether or not in British Columbia or another jurisdiction, been suspended or cancelled <u>for a matter involving an “unlawful or improper action”</u>?", EffectiveDate = Dec2022EffectiveDate, CreatedTimeStamp = SEEDING_DATE, UpdatedTimeStamp = SEEDING_DATE },
                };
            }
        }
    }
}

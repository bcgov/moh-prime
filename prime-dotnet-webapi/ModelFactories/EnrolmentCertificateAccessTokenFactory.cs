using System;
using Bogus;
using Prime.Models;

namespace Prime.ModelFactories
{
    public class EnrolmentCertificateAccessTokenFactory : Faker<EnrolmentCertificateAccessToken>
    {
        public EnrolmentCertificateAccessTokenFactory(Enrollee owner)
        {
            this.SetBaseRules();

            RuleFor(x => x.Id, f => Guid.NewGuid());
            RuleFor(x => x.Enrollee, f => owner);
            RuleFor(x => x.EnrolleeId, f => owner.Id);
            RuleFor(x => x.Expires, f => f.Date.Future());
            RuleFor(x => x.ViewCount, 0);
            RuleFor(x => x.Active, true);

            RuleSet("inactive", (set) =>
            {
                set.RuleFor(x => x.Active, false);
            });
            RuleSet("expired", (set) =>
            {
                set.RuleFor(x => x.Expires, f => f.Date.Past());
            });
            RuleSet("maxViews", (set) =>
            {
                set.RuleFor(x => x.ViewCount, 3);
            });
        }
    }
}

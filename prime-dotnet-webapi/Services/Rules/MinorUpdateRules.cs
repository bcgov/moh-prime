using System;
using System.Linq;
using System.Threading.Tasks;
using KellermanSoftware.CompareNetObjects;
using KellermanSoftware.CompareNetObjects.TypeComparers;
using Prime.Models;
using Prime.ViewModels;


namespace Prime.Services.Rules
{
    /// <summary>
    /// Conditions under which a submission is considered to be minor enough to not warrant going through the (Auto) adjudication proccess.
    /// Does not alter the enrollee object.
    /// </summary>
    public abstract class MinorUpdateRule : IEnrolleeRule
    {
        public abstract Task<bool> ProcessRule(Enrollee enrollee);
    }

    /// <summary>
    /// Enrollee has a signed TOA and it is the newest verson
    /// </summary>
    public class CurrentToaRule : MinorUpdateRule
    {
        private readonly IAccessTermService _accessTermService;

        public CurrentToaRule(IAccessTermService accessTermService)
        {
            _accessTermService = accessTermService;
        }

        public override async Task<bool> ProcessRule(Enrollee enrollee)
        {
            if (enrollee.AccessTerms == null)
            {
                return false;
            }

            var signedToa = enrollee.AccessTerms
                .OrderByDescending(at => at.AcceptedDate)
                .FirstOrDefault(at => at.AcceptedDate != null);

            if (signedToa == null)
            {
                return false;
            }

            return await _accessTermService.IsCurrentAsync(signedToa.Id);
        }
    }

    /// <summary>
    /// Update must be more than 90 days away from renewal date
    /// </summary>
    public class DateRule : MinorUpdateRule
    {
        public override Task<bool> ProcessRule(Enrollee enrollee)
        {
            if (enrollee.ExpiryDate == null)
            {
                return Task.FromResult(true);
            }

            TimeSpan diff = enrollee.ExpiryDate.Value - DateTimeOffset.Now;
            return Task.FromResult(diff > TimeSpan.FromDays(90));
        }
    }

    /// <summary>
    /// Update must only change certain allowed properties
    /// </summary>
    public class AllowableChangesRule : MinorUpdateRule
    {
        private readonly EnrolleeProfileViewModel _updatedProfile;

        public AllowableChangesRule(EnrolleeProfileViewModel updatedProfile)
        {
            _updatedProfile = updatedProfile;
        }

        public override Task<bool> ProcessRule(Enrollee enrollee)
        {
            var comparitor = InitComparitor(enrollee);

            if (!comparitor.Compare(enrollee, _updatedProfile).AreEqual)
            {
                return Task.FromResult(false);
            }

            comparitor.Config.IgnoreObjectTypes = false; // To match collection objects

            if (!comparitor.Compare(enrollee.Certifications.ToArray(), _updatedProfile.Certifications.ToArray()).AreEqual)
            {
                return Task.FromResult(false);
            }

            if (enrollee.IsObo != true // OBOs can change Job titles
                && !comparitor.Compare(enrollee.Jobs.ToArray(), _updatedProfile.Jobs.ToArray()).AreEqual)
            {
                return Task.FromResult(false);
            }

            if (!comparitor.Compare(enrollee.Organizations.ToArray(), _updatedProfile.Organizations.ToArray()).AreEqual)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        private static CompareLogic InitComparitor(Enrollee enrollee)
        {
            ComparisonConfig config = new ComparisonConfig();
            config.IgnoreObjectTypes = true; // To match Enrollee to EnrolleeViewModel
            config.CompareFields = false;
            config.MaxDifferences = 100;
            config.IgnoreCollectionOrder = true;

            // Fields considered "minor" changes
            config.IgnoreProperty<Enrollee>(x => x.ContactEmail);
            config.IgnoreProperty<Enrollee>(x => x.ContactPhone);
            config.IgnoreProperty<Enrollee>(x => x.VoicePhone);
            config.IgnoreProperty<Enrollee>(x => x.VoiceExtension);

            config.CustomComparers.Add(new CertificationComparer(RootComparerFactory.GetRootComparer()));
            config.CustomComparers.Add(new JobComparer(RootComparerFactory.GetRootComparer()));
            config.CustomComparers.Add(new MailingAddressComparer(RootComparerFactory.GetRootComparer()));
            config.CustomComparers.Add(new OrganizationComparer(RootComparerFactory.GetRootComparer()));

            return new CompareLogic(config);
        }

        private class CertificationComparer : BaseTypeComparer
        {
            public CertificationComparer(RootComparer rootComparer) : base(rootComparer) { }

            public override bool IsTypeMatch(Type type1, Type type2)
            {
                return type1 == typeof(Certification);
            }

            public override void CompareType(CompareParms parms)
            {
                var cert1 = (Certification)parms.Object1;
                var cert2 = (Certification)parms.Object2;

                if (cert1.CollegeCode != cert2.CollegeCode
                    || cert1.LicenseNumber != cert2.LicenseNumber
                    || cert1.LicenseCode != cert2.LicenseCode
                    || cert1.CollegeCode != cert2.CollegeCode
                    || cert1.RenewalDate != cert2.RenewalDate
                    || cert1.PracticeCode != cert2.PracticeCode)
                {
                    AddDifference(parms);
                }
            }
        }

        private class JobComparer : BaseTypeComparer
        {
            public JobComparer(RootComparer rootComparer) : base(rootComparer) { }

            public override bool IsTypeMatch(Type type1, Type type2)
            {
                return type1 == typeof(Job);
            }

            public override void CompareType(CompareParms parms)
            {
                var job1 = (Job)parms.Object1;
                var job2 = (Job)parms.Object2;

                if (job1.Title != job2.Title)
                {
                    AddDifference(parms);
                }
            }
        }

        private class MailingAddressComparer : BaseTypeComparer
        {
            public MailingAddressComparer(RootComparer rootComparer) : base(rootComparer) { }

            public override bool IsTypeMatch(Type type1, Type type2)
            {
                return type1 == typeof(MailingAddress);
            }

            public override void CompareType(CompareParms parms)
            {
                var addr1 = (MailingAddress)parms.Object1;
                var addr2 = (MailingAddress)parms.Object2;

                if (addr1.CountryCode != addr2.CountryCode
                    || addr1.ProvinceCode != addr2.ProvinceCode
                    || addr1.Street != addr2.Street
                    || addr1.Street2 != addr2.Street2
                    || addr1.City != addr2.City
                    || addr1.Postal != addr2.Postal)
                {
                    AddDifference(parms);
                }
            }
        }

        private class OrganizationComparer : BaseTypeComparer
        {
            public OrganizationComparer(RootComparer rootComparer) : base(rootComparer) { }

            public override bool IsTypeMatch(Type type1, Type type2)
            {
                return type1 == typeof(Organization);
            }

            public override void CompareType(CompareParms parms)
            {
                var org1 = (Organization)parms.Object1;
                var org2 = (Organization)parms.Object2;

                if (org1.OrganizationTypeCode != org2.OrganizationTypeCode)
                {
                    AddDifference(parms);
                }
            }
        }
    }
}

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
        private EnrolleeProfileViewModel _updatedProfile;

        public AllowableChangesRule(EnrolleeProfileViewModel updatedProfile)
        {
            _updatedProfile = updatedProfile;
        }

        public override Task<bool> ProcessRule(Enrollee enrollee)
        {
            var comparitor = InitComparitor();
            var result = comparitor.Compare(_updatedProfile, enrollee);

            if (result.AreEqual)
            {
                return Task.FromResult(true);
            }

            foreach (var diff in result.Differences)
            {
                if (true // TODO enrollee is OBO
                    && diff.PropertyName == nameof(Enrollee.Jobs))
                {
                    // OBOs can change their job titles(s)
                    continue;
                }

                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        private static CompareLogic InitComparitor()
        {
            ComparisonConfig config = new ComparisonConfig();
            config.IgnoreObjectTypes = true;
            config.CompareFields = false;
            config.MaxDifferences = 100;
            config.IgnoreCollectionOrder = true;

            // Fields considered "minor" changes
            config.IgnoreProperty<Enrollee>(x => x.ContactEmail);
            config.IgnoreProperty<Enrollee>(x => x.ContactPhone);
            config.IgnoreProperty<Enrollee>(x => x.VoicePhone);
            config.IgnoreProperty<Enrollee>(x => x.VoiceExtension);

            // Ignored fields on child models due to how the frontend sends objects.
            config.IgnoreProperty<BaseAuditable>(x => x.CreatedUserId);
            config.IgnoreProperty<BaseAuditable>(x => x.CreatedTimeStamp);
            config.IgnoreProperty<BaseAuditable>(x => x.UpdatedUserId);
            config.IgnoreProperty<BaseAuditable>(x => x.UpdatedTimeStamp);

            config.IgnoreProperty<Certification>(x => x.Id);
            config.IgnoreProperty<Certification>(x => x.EnrolleeId);
            config.IgnoreProperty<Certification>(x => x.Enrollee);
            config.IgnoreProperty<Certification>(x => x.College);
            config.IgnoreProperty<Certification>(x => x.License);
            config.IgnoreProperty<Certification>(x => x.Practice);
            config.IgnoreProperty<Certification>(x => x.FullLicenseNumber);

            config.IgnoreProperty<Job>(x => x.Id);
            config.IgnoreProperty<Job>(x => x.EnrolleeId);
            config.IgnoreProperty<Job>(x => x.Enrollee);

            config.IgnoreProperty<MailingAddress>(x => x.Id);
            config.IgnoreProperty<MailingAddress>(x => x.EnrolleeId);
            config.IgnoreProperty<MailingAddress>(x => x.Enrollee);
            config.IgnoreProperty<MailingAddress>(x => x.Country);
            config.IgnoreProperty<MailingAddress>(x => x.Province);

            return new CompareLogic(config);
        }
    }
}

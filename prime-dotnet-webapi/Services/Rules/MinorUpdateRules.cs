using System;
using System.Linq;
using System.Threading.Tasks;
using KellermanSoftware.CompareNetObjects;
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
            CompareLogic compareLogic = new CompareLogic();
            compareLogic.Config.IgnoreObjectTypes = true;
            compareLogic.Config.CompareFields = false;
            compareLogic.Config.MaxDifferences = 10;
            compareLogic.Config.IgnoreCollectionOrder = true;

            var result = compareLogic.Compare(_updatedProfile, enrollee);

            if (result.AreEqual)
            {
                return Task.FromResult(true);
            }

            var ignoredProperties = new[]
            {
                nameof(Enrollee.ContactEmail),
                nameof(Enrollee.ContactPhone),
                nameof(Enrollee.VoicePhone),
                nameof(Enrollee.VoiceExtension)
            };

            foreach (var diff in result.Differences)
            {
                if (ignoredProperties.Contains(diff.PropertyName))
                {
                    continue;
                }
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
    }
}

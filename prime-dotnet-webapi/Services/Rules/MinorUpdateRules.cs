using System;
using System.Collections.Generic;
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

            // Now compare all collection properties
            comparitor.Config.IgnoreObjectTypes = false;

            if (!CompareCollections(comparitor, enrollee.Certifications, _updatedProfile.Certifications))
            {
                return Task.FromResult(false);
            }

            if (enrollee.IsObo != true // OBOs can change Job titles
                && !CompareCollections(comparitor, enrollee.Jobs, _updatedProfile.Jobs))
            {
                return Task.FromResult(false);
            }

            if (!CompareCollections(comparitor, enrollee.Organizations, _updatedProfile.Organizations))
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
            if (enrollee.IsObo == true)
            {
                config.IgnoreProperty<Enrollee>(x => x.Jobs);
            }

            // Ignored fields on models due to the frontend not sending all keys/navigation properties
            config.IgnoreProperty<BaseAuditable>(x => x.CreatedUserId);
            config.IgnoreProperty<BaseAuditable>(x => x.CreatedTimeStamp);
            config.IgnoreProperty<BaseAuditable>(x => x.UpdatedUserId);
            config.IgnoreProperty<BaseAuditable>(x => x.UpdatedTimeStamp);

            config.IgnoreProperty<Certification>(x => x.Id);
            config.IgnoreProperty<Certification>(x => x.Enrollee);
            config.IgnoreProperty<Certification>(x => x.EnrolleeId);
            config.IgnoreProperty<Certification>(x => x.College);
            config.IgnoreProperty<Certification>(x => x.License);
            config.IgnoreProperty<Certification>(x => x.Practice);

            config.IgnoreProperty<Job>(x => x.Id);
            config.IgnoreProperty<Job>(x => x.Enrollee);
            config.IgnoreProperty<Job>(x => x.EnrolleeId);

            config.IgnoreProperty<MailingAddress>(x => x.Id);
            config.IgnoreProperty<MailingAddress>(x => x.Enrollee);
            config.IgnoreProperty<MailingAddress>(x => x.EnrolleeId);
            config.IgnoreProperty<MailingAddress>(x => x.Country);
            config.IgnoreProperty<MailingAddress>(x => x.Province);

            config.IgnoreProperty<Organization>(x => x.Id);
            config.IgnoreProperty<Organization>(x => x.Enrollee);
            config.IgnoreProperty<Organization>(x => x.EnrolleeId);
            config.IgnoreProperty<Organization>(x => x.OrganizationType);

            return new CompareLogic(config);
        }

        private static bool CompareCollections<T>(CompareLogic comparitor, ICollection<T> coll1, ICollection<T> coll2)
        {
            if (coll1 == null && coll2 == null)
            {
                return true;
            }

            if ((coll1 == null) || (coll2 == null))
            {
                // Exactly one is null
                return false;
            }

            if (coll1.Count != coll2.Count)
            {
                return false;
            }

            foreach (T item in coll1)
            {
                if (!coll2.Any(i => comparitor.Compare(item, i).AreEqual))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

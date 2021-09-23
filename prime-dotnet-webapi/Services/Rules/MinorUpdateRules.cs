using System;
using System.Linq;
using System.Collections.Generic;
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
        public override Task<bool> ProcessRule(Enrollee enrollee)
        {
            if (enrollee.Agreements == null)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(enrollee.HasLatestAgreement());
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
    /// Update must only change certain allowed properties.
    /// See `InitComparitor` method to see what is considered a minor update.
    /// </summary>
    public class AllowableChangesRule : MinorUpdateRule
    {
        private readonly EnrolleeUpdateModel _updatedProfile;

        public AllowableChangesRule(EnrolleeUpdateModel updatedProfile)
        {
            _updatedProfile = updatedProfile;
        }

        public override Task<bool> ProcessRule(Enrollee enrollee)
        {
            var comparitor = InitComparitor();

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

            if (!CompareCollections(comparitor, enrollee.OboSites, _updatedProfile.OboSites))
            {
                return Task.FromResult(false);
            }

            if (!CompareCollections(comparitor, enrollee.EnrolleeCareSettings, _updatedProfile.EnrolleeCareSettings))
            {
                return Task.FromResult(false);
            }

            if (!CompareCollections(comparitor, enrollee.EnrolleeHealthAuthorities, _updatedProfile.EnrolleeHealthAuthorities))
            {
                return Task.FromResult(false);
            }

            if (!CompareCollections(comparitor, enrollee.EnrolleeRemoteUsers, _updatedProfile.EnrolleeRemoteUsers))
            {
                return Task.FromResult(false);
            }

            if (!CompareCollections(comparitor, enrollee.RemoteAccessSites, _updatedProfile.RemoteAccessSites))
            {
                return Task.FromResult(false);
            }

            if (!CompareCollections(comparitor, enrollee.RemoteAccessLocations, _updatedProfile.RemoteAccessLocations))
            {
                return Task.FromResult(false);
            }

            // If enrollee selected that they had previous paper enrolment and they provided their GPID
            // that was provided to them during the papoer enrolment, then check to see if this
            // if (_updatedProfile.GPID != null)
            // {
            //     return Task.FromResult(false);
            // }

            // If the new profile has self declaration document GUIDs in it, the user has uploaded new documents
            if (_updatedProfile.SelfDeclarations.Any(sd => (sd.DocumentGuids != null && sd.DocumentGuids.Any()))
                || !CompareCollections(comparitor, enrollee.SelfDeclarations, _updatedProfile.SelfDeclarations))
            {
                return Task.FromResult(false);
            }


            return Task.FromResult(true);
        }

        private static CompareLogic InitComparitor()
        {
            ComparisonConfig config = new ComparisonConfig
            {
                IgnoreObjectTypes = true, // To match Enrollee to EnrolleeViewModel
                CompareFields = false,
                MaxDifferences = 100,
                IgnoreCollectionOrder = true
            };

            // Fields considered "minor" changes
            config.IgnoreProperty<Enrollee>(x => x.Email);
            config.IgnoreProperty<Enrollee>(x => x.SmsPhone);
            config.IgnoreProperty<Enrollee>(x => x.Phone);
            config.IgnoreProperty<Enrollee>(x => x.PhoneExtension);
            config.IgnoreProperty<OboSite>(x => x.JobTitle);

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

            config.IgnoreProperty<OboSite>(x => x.Id);
            config.IgnoreProperty<OboSite>(x => x.Enrollee);
            config.IgnoreProperty<OboSite>(x => x.EnrolleeId);
            config.IgnoreProperty<OboSite>(x => x.CareSetting);
            config.IgnoreProperty<OboSite>(x => x.HealthAuthority);

            config.IgnoreProperty<Address>(x => x.Id);
            config.IgnoreProperty<Address>(x => x.Country);
            config.IgnoreProperty<Address>(x => x.Province);

            config.IgnoreProperty<EnrolleeCareSetting>(x => x.Id);
            config.IgnoreProperty<EnrolleeCareSetting>(x => x.Enrollee);
            config.IgnoreProperty<EnrolleeCareSetting>(x => x.EnrolleeId);
            config.IgnoreProperty<EnrolleeCareSetting>(x => x.CareSetting);

            config.IgnoreProperty<EnrolleeRemoteUser>(x => x.Id);
            config.IgnoreProperty<EnrolleeRemoteUser>(x => x.Enrollee);
            config.IgnoreProperty<EnrolleeRemoteUser>(x => x.RemoteUser);

            config.IgnoreProperty<RemoteAccessSite>(x => x.Id);
            config.IgnoreProperty<RemoteAccessSite>(x => x.Enrollee);
            config.IgnoreProperty<RemoteAccessSite>(x => x.Site);

            config.IgnoreProperty<RemoteAccessLocation>(x => x.Id);
            config.IgnoreProperty<RemoteAccessLocation>(x => x.Enrollee);
            config.IgnoreProperty<RemoteAccessLocation>(x => x.EnrolleeId);

            config.IgnoreProperty<SelfDeclaration>(x => x.Id);
            config.IgnoreProperty<SelfDeclaration>(x => x.EnrolleeId);
            config.IgnoreProperty<SelfDeclaration>(x => x.Enrollee);
            config.IgnoreProperty<SelfDeclaration>(x => x.SelfDeclarationType);
            config.IgnoreProperty<SelfDeclaration>(x => x.DocumentGuids);

            config.IgnoreProperty<EnrolleeHealthAuthority>(x => x.Id);
            config.IgnoreProperty<EnrolleeHealthAuthority>(x => x.EnrolleeId);
            config.IgnoreProperty<EnrolleeHealthAuthority>(x => x.Enrollee);
            config.IgnoreProperty<EnrolleeHealthAuthority>(x => x.HealthAuthority);

            return new CompareLogic(config);
        }

        private static bool CompareCollections<T>(CompareLogic comparitor, ICollection<T> coll1, ICollection<T> coll2)
        {
            if (coll1 == null && coll2 == null)
            {
                return true;
            }

            if (coll1 == null || coll2 == null)
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

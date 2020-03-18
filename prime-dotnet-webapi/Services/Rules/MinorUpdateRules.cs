using System;
using System.Linq;
using System.Threading.Tasks;
using Prime.Models;

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
        public override Task<bool> ProcessRule(Enrollee enrollee)
        {
            throw new System.NotImplementedException();
        }
    }
}

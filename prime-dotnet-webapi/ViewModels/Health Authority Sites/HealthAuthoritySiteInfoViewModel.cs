using System;
using FluentValidation;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteInfoViewModel
    {
        public string SiteName { get; set; }

        public string SiteId { get; set; }

        public string SecurityGroup { get; set; }
    }

    public class HealthAuthoritySiteInfoValidator : AbstractValidator<HealthAuthoritySiteInfoViewModel>
    {
        public HealthAuthoritySiteInfoValidator()
        {
            RuleFor(x => x.SiteName).NotNull();
            RuleFor(x => x.SiteId).NotNull();
            RuleFor(x => x.SecurityGroup).NotNull();
        }
    }
}

using System;
using FluentValidation;
using Prime.Models.HealthAuthorities;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteAdministratorViewModel
    {
        public int HealthAuthorityPharmanetAdministratorId { get; set; }
    }

    public class HealthAuthoritySiteAdministratorValidator : AbstractValidator<HealthAuthoritySiteAdministratorViewModel>
    {
        public HealthAuthoritySiteAdministratorValidator()
        {
            RuleFor(x => x.HealthAuthorityPharmanetAdministratorId).NotNull();
        }
    }
}

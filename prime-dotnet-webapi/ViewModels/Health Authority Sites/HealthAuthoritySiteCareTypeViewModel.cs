using System;
using FluentValidation;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteCareTypeViewModel
    {
        public int HealthAuthorityCareTypeId { get; set; }
    }

    public class HealthAuthoritySiteCareTypeValidator : AbstractValidator<HealthAuthoritySiteCareTypeViewModel>
    {
        // TODO when does the validator get invoked?
        public HealthAuthoritySiteCareTypeValidator()
        {
            RuleFor(x => x.HealthAuthorityCareTypeId).NotNull();
        }
    }
}

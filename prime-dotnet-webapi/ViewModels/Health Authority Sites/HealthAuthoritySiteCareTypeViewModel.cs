using System;
using FluentValidation;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthorityCareTypeViewModel
    {
        public int Id { get; set; }
        public string CareType { get; set; }
    }

    public class HealthAuthorityCareTypeValidator : AbstractValidator<HealthAuthorityCareTypeViewModel>
    {
        public HealthAuthorityCareTypeValidator()
        {
            RuleFor(x => x.CareType).NotNull();
        }
    }
}

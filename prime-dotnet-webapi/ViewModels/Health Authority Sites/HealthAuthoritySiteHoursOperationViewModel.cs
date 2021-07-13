using System;
using System.Collections.Generic;
using FluentValidation;
using Prime.Models;

namespace Prime.ViewModels.HealthAuthoritySites
{
    public class HealthAuthoritySiteHoursOperationViewModel
    {
        public ICollection<BusinessDay> BusinessHours { get; set; }
    }

    public class HealthAuthoritySiteHoursOperationValidator : AbstractValidator<HealthAuthoritySiteHoursOperationViewModel>
    {
        public HealthAuthoritySiteHoursOperationValidator()
        {
            RuleFor(x => x.BusinessHours).NotNull();
        }
    }
}
